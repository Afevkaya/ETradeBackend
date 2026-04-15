namespace ETradeBackend.Application.Exceptions;

public class InvalidBasketItemQuantityException : Exception
{
    public InvalidBasketItemQuantityException() : base("Sepet öğesi miktarı geçersiz!")
    {
        
    }
    public InvalidBasketItemQuantityException(string message) : base(message)
    {
        
    }
    public InvalidBasketItemQuantityException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}