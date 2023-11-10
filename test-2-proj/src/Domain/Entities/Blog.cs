using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("blogs")]
public class Blog : BaseAuditableEntity
{
    [Column("title")] public string? Title { get; set; }

    [Column("content")] public string? Content { get; set; }

    [Column("author_id")] public string? AuthorId { get; set; }

    [Column("status")] public BlogStatus Status { get; set; }
}