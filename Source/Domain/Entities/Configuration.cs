using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("configuration")]
public class Configuration : BaseAuditableEntity
{
}
