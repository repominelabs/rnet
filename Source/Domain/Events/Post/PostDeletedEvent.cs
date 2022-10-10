using Domain.Common;

namespace Domain.Events.Post;

public class PostDeletedEvent : BaseEvent
{
	public Entities.Post Post { get; set; }

	public PostDeletedEvent(Entities.Post post)
	{
		Post = post;
	}
}
