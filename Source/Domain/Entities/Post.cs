using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("posts")]
public class Post : BaseAuditableEntity
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("body")]
    public string Body { get; set; }

    public List<Comment> Comments { get; set; }
}