using Domain.Common;

namespace Domain.Events.Comment;

public class CommentCreatedEvent : BaseEvent
{
    public Entities.Comment Comment { get; }

    public CommentCreatedEvent(Entities.Comment comment)
    {
        Comment = comment;
    }
}
