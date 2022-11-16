using Application.Interfaces.Repositories.Dapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Dapper;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(string connStr) : base(connStr)
    {
    }
}
