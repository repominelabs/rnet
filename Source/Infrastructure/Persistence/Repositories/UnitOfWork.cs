using Application.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IPostRepository Posts { get; }
    public ICommentRepository Comments { get; }

    public UnitOfWork(IPostRepository Posts, ICommentRepository Comments)
    {
        this.Posts = Posts;
        this.Comments = Comments;
    }
}
