using Microsoft.AspNetCore.WebUtilities;

namespace ETradeBackend.Application.Helpers;

public static class CustomEncoders
{
    public static string UrlEncode(this string value)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        return WebEncoders.Base64UrlEncode(bytes);
    }
    
    public static string UrlDecode(this string value)
    {
        var bytes = WebEncoders.Base64UrlDecode(value);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}