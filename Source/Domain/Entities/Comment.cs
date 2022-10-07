using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
