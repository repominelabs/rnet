using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("configuration")]
public class Configuration : BaseAuditableEntity
{
    [Column("application")]
    public string Application { get; set; }

    [Column("config_type")]
    public string ConfigType { get; set; }

    [Column("config_value_1")]
    public string ConfigValue1 { get; set; }

    [Column("config_value_2")]
    public string ConfigValue2 { get; set; }

    [Column("key")]
    public string Key { get; set; }

    [Column("value")]
    public string Value { get; set; }

    [Column("is_active")]
    public string IsActive { get; set; }

    [Column("description")]
    public string Description { get; set; }
}
