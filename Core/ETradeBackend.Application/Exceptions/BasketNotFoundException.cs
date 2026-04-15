namespace ETradeBackend.Application.Exceptions;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException() : base("Sepet öğesi bulunamadı!")
    {
        
    }
    public BasketNotFoundException(string message) : base(message)
    {
        
    }
    public BasketNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}