using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.Files;

public class FileWriteRepository(ETradeDbContext eTradeDbContext) : WriteRepository<ETradeBackend.Domain.Entities.Files.File>(eTradeDbContext), IFileWriteRepository
{
}
