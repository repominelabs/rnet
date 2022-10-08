using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("post")]
public class Post : BaseAuditableEntity
{
    [Column("user_id")]
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}