using Domain.Common;

namespace Domain.Events.Post;

public class PostCreatedEvent : BaseEvent
{
	public Entities.Post Post { get; }

	public PostCreatedEvent(Entities.Post post)
	{
		Post = post;
	}
}
