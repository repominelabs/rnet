using Application.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// 
/// </summary>
public class BaseRepository<T> : IBaseRepository<T>
{
    private readonly string _connStr;

    public BaseRepository(string connStr)
    {
        _connStr = connStr;
    }
}
