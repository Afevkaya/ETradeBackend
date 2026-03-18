using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Files;

public class FileReadRepository(ETradeDbContext eTradeDbContext) : ReadRepository<ETradeBackend.Domain.Entities.Files.File>(eTradeDbContext), IFileReadRepository
{
}
