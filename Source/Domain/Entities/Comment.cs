using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("comment")]
public class Comment : BaseAuditableEntity
{
    [Column("post_id")]
    public int PostId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("body")]
    public string Body { get; set; }
}
