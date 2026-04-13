using MediatR;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.ChangeShowcaseImage;

public record ChangeShowcaseImageCommandRequest(Guid ProductId, Guid ImageId) : IRequest<ChangeShowcaseImageCommandResponse>;