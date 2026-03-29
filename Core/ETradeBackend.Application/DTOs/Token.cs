namespace ETradeBackend.Application.DTOs;

public record Token
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}