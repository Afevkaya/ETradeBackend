using System.Net;
using System.Net.Mail;
using System.Text;
using ETradeBackend.Application.Abstractions.Services;
using Microsoft.Extensions.Options;

namespace ETradeBackend.Infrastructure.Services.Mails;

public class MailService(IOptions<EmailSettings> emailSettings) : IMailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SenMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SenMessageAsync([to], subject, body, isBodyHtml);
    }

    public async Task SenMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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
}