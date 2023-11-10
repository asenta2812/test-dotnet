using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("created_by")] public string? CreatedBy { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")] public string? UpdatedBy { get; set; }
}