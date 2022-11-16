using Application.Interfaces.Repositories.Dapper;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Dapper;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(string connStr) : base(connStr)
    {
    }
}
