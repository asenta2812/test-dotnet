using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("authors")]
public class Author : BaseAuditableEntity
{
    [Column("username")] public string? Username { get; set; }

    [Column("password")] public string? Password { get; set; }
}