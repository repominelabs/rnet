using Application.Interfaces.Repositories.Dapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories.Dapper;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(string connStr) : base(connStr)
    {
    }
}
