using Application.Interfaces.Repositories;
using Cinis.PostgreSql;
using Npgsql;

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

    public dynamic Create(T entity)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            dynamic response = connection.Create(entity);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<dynamic> CreateAsync(T entity)
    {
        using var connection = new NpgsqlConnection(_connStr);
        await connection.OpenAsync();
        try
        {
            dynamic response = await connection.CreateAsync(entity);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public dynamic CreateOrUpdate(T entity, bool nullable = false, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Delete(dynamic id, string whereClause)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            dynamic response = connection.Delete<T>(id: id, whereClause: whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public Task<dynamic> DeleteAsync(dynamic? id = null, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Read(dynamic? id = null, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> ReadAsync(dynamic? id = null, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Update(T entity, bool nullable = false, string? whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> UpdateAsync(T entity, bool nullable = false, string? whereClause = null)
    {
        throw new NotImplementedException();
    }
}
