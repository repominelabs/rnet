using Domain.Common;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
