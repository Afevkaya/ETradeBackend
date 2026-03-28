using ETradeBackend.Application.Repositories.Products;
using MediatR;

namespace ETradeBackend.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQueryHandler(
    IProductReadRepository productReadRepository) : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await productReadRepository.GetByIdAsync(request.Id,false);
        var response = new GetByIdProductQueryResponse(result.Name, result.Price, result.Stock);
        return response;
    }
}