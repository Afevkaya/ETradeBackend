namespace ETradeBackend.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Kullanıcı adı veya şifre yanlış!")
    {
        
    }
    
    public UserNotFoundException(string message) : base(message)
    {
        
    }
    
    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}