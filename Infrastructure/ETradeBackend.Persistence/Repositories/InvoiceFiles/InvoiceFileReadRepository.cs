using ETradeBackend.Application.Repositories.InvoiceFiles;
using ETradeBackend.Domain.Entities.Files;
using ETradeBackend.Persistence.Contexts;

namespace ETradeBackend.Persistence.Repositories.InvoiceFiles;

public class InvoiceFileReadRepository(ETradeDbContext eTradeDbContext) : ReadRepository<InvoiceFile>(eTradeDbContext), IInvoiceFileReadRepository
{
}

