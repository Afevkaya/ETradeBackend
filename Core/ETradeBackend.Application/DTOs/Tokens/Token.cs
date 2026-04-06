namespace ETradeBackend.Application.DTOs.Tokens;

public record Token
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
}