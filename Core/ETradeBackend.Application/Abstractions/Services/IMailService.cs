namespace ETradeBackend.Application.Abstractions.Services;

public interface IMailService
{
    Task SenMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
    Task SenMessageAsync(string[] to, string subject, string body, bool isBodyHtml = true);
}