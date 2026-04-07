using ETradeBackend.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ETradeBackend.Application.Features.Queries.Products.GetAllProduct;

public class GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger) : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = productReadRepository.GetAll(false).Count();
        var products = await productReadRepository
            .GetAll(false)
            .Select(p=> new {p.Id, p.Name, p.Price, p.Stock, p.CreatedAt, p.UpdatedAt})
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        
        logger.LogInformation("Get all products");
        return new GetAllProductQueryResponse(totalCount, products);
    }
}