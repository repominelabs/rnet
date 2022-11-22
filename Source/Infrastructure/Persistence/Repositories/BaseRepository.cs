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

    public object Create(T entity)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            object response = connection.Create(entity);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<object> CreateAsync(T entity)
    {
        using var connection = new NpgsqlConnection(_connStr);
        await connection.OpenAsync();
        try
        {
            object response = await connection.CreateAsync(entity);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public object CreateOrUpdate(T entity, bool nullable = false, object? id = null, string? whereClause = null)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        T existingEntity = Read(id, whereClause).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (existingEntity != null)
        {
            object response = Update(entity, nullable, whereClause);
            return response;
        }
        else
        {
            object response = Create(entity);
            return response;
        }
    }

    public async Task<object> CreateOrUpdateAsync(T entity, bool nullable = false, object? id = null, string? whereClause = null)
    {
        var existingEntities = await ReadAsync(id, whereClause);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        T existingEntity = existingEntities.FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (existingEntity != null)
        {
            object response = await UpdateAsync(entity, nullable, whereClause);
            return response;
        }
        else
        {
            object response = await CreateAsync(entity);
            return response;
        }
    }

    public object Delete(object id, string whereClause)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            object response = connection.Delete<T>(id, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<object> DeleteAsync(object? id = null, string? whereClause = null)
    {
        using var connection = new NpgsqlConnection(_connStr);
        await connection.OpenAsync();
        try
        {
            object response = await connection.DeleteAsync<T>(id, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public List<T> Read(object? id = null, string? whereClause = null)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            List<T> response = connection.Read<T>(id, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<List<T>> ReadAsync(object? id = null, string? whereClause = null)
    {
        using var connection = new NpgsqlConnection(_connStr);
        await connection.OpenAsync();
        try
        {
            List<T> response = await connection.ReadAsync<T>(id, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public object Update(T entity, bool nullable = false, string? whereClause = null)
    {
        using var connection = new NpgsqlConnection(_connStr);
        connection.Open();
        try
        {
            object response = connection.Update(entity, nullable, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<object> UpdateAsync(T entity, bool nullable = false, string? whereClause = null)
    {
        using var connection = new NpgsqlConnection(_connStr);
        await connection.OpenAsync();
        try
        {
            object response = await connection.UpdateAsync(entity, nullable, whereClause);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
