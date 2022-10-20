using Application.Interfaces.DatabaseManagers;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(string connStr, IDatabaseManager databaseManager) : base(connStr, databaseManager)
    {
    }
}
