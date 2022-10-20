using Application.Interfaces.DatabaseManagers;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(string connStr, IDatabaseManager databaseManager) : base(connStr, databaseManager)
    {
    }
}
