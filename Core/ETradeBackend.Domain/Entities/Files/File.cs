using System.ComponentModel.DataAnnotations.Schema;
using ETradeBackend.Domain.Entities.Common;

namespace ETradeBackend.Domain.Entities.Files;

public class File : BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    [NotMapped]
    public override DateTime? UpdatedAt { get; set; }
}