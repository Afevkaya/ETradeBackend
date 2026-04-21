using System.Net;
using System.Net.Mail;
using System.Text;
using ETradeBackend.Application.Abstractions.Services;
using Microsoft.Extensions.Options;

namespace ETradeBackend.Infrastructure.Services.Mails;

public class MailService(IOptions<EmailSettings> emailSettings) : IMailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SendMailAsync([to], subject, body, isBodyHtml);
    }

    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        var mailMessage = new MailMessage();
        mailMessage.IsBodyHtml = isBodyHtml;
        foreach (var to in tos)
            mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName, Encoding.UTF8);

        using var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
        smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
        smtp.EnableSsl = true;
        smtp.Host = _emailSettings.Host;
        smtp.Port = _emailSettings.Port;
        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendResetPasswordMailAsync(string to, Guid userId, string resetToken)
    {
        var resetUrl = $"{_emailSettings.ResetPasswordBaseUrl}?userId={Uri.EscapeDataString(userId.ToString())}&token={Uri.EscapeDataString(resetToken)}";

        var builder = new StringBuilder();
        builder.AppendLine("Merhaba<br>");
        builder.AppendLine("Eğer yeni bir şifre talebinde bulunduysanız aşağıdaki linke tıklayarak şifrenizi sıfırlayabilirsiniz.<br>");
        builder.AppendLine($"<strong><a target=\"_blank\" href=\"{resetUrl}\">Şifre Sıfırlama Linki</a></strong><br><br>");
        builder.AppendLine("Eğer bu talebi siz yapmadıysanız, lütfen bu e-postayı görmezden gelin.<br><br>");
        builder.AppendLine("İyi günler dileriz.");

        await SendMailAsync(to, "Şifre Yenileme Talebi", builder.ToString());
    }

    public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string nameSurname)
    {
        var mail = $"Sayın {nameSurname} Merhaba <br> {orderDate} tarihinde vermiş olduğunuz {orderCode} kodulu"
                     + " siparişiniz başarıyla tamamlanmıştır. <br> Siparişinizle ilgili detayları hesabınızdan inceleyebilirsiniz. <br><br>"
                     + "ETrade Backend Ekibi";
        await SendMailAsync(to, "Siparişiniz Tamamlandı", mail);
    }
}