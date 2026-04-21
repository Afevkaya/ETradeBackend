namespace ETradeBackend.Application.Exceptions;

public class PasswordChangeFailedException : Exception
{    
    public PasswordChangeFailedException() : base("Şifre güncelleme sırasında bir hata oluştu")
    {
    }

    public PasswordChangeFailedException(string message) : base(message)
    {
    }

    public PasswordChangeFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}