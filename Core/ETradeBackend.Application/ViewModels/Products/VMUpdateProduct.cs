namespace ETradeBackend.Application.ViewModels.Products;

public record VMUpdateProduct(Guid Id, string Name, decimal Price, int Stock);