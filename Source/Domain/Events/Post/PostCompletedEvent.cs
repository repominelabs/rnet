using Domain.Common;

namespace Domain.Events.Post;

public class PostCompletedEvent : BaseEvent
{
    public Entities.Post Post { get; }

    public PostCompletedEvent(Entities.Post post)
    {
        Post = post;
    }
}
