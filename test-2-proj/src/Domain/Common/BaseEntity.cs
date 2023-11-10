using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity
{
    [Column("id")] public int Id { get; set; }

    [Column("is_deleted")] public bool IsDeleted { get; set; }
}