using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Dapper;

namespace Infrastructure.Persistence.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(string connStr) : base(connStr)
    {
    }
}
