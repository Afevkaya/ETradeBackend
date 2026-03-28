using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Application.Features.Commands.ProductImageFiles.UploadImageFile;

public class UploadImageFileCommandRequest : IRequest<UploadImageFileCommandResponse>
{
    public Guid ProductId { get; set; }
    public IFormFileCollection FormFileCollection { get; set; }
} 