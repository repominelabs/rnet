namespace Application.Interfaces.Repositories.Dapper;

public interface IUnitOfWork
{
    IPostRepository Posts { get; }
    ICommentRepository Comments { get; }
}
