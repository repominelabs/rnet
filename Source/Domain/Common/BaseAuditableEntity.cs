using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("created_by")]
    public string CreatedBy { get; set; }

    [Column("last_modified_date")]
    public DateTime LastModifiedDate { get; set; }

    [Column("last_modified_by")]
    public string LastModifiedBy { get; set; }
}
