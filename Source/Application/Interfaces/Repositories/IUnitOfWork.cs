namespace Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
    }
}
