using Application.Interfaces.Repositories;
using Cinis.PostgreSql;
using Infrastructure.Persistence.Common;
using Npgsql;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// 
/// </summary>
public abstract class BaseRepository<T> : IBaseRepository<T>
{
    private readonly string _connStr;

    public BaseRepository(string connStr)
    {
        _connStr = connStr;
    }

    public BaseRepository()
    {
    }

    public dynamic Create(T entity, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                connection.Open();
                result = connection.Create(entity, transaction);
            }
        }
        else
        {
            result = connection.Create(entity, transaction);
        }

        return result;
    }

    public async Task<dynamic> CreateAsync(T entity, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                await connection.OpenAsync();
                result = await connection.CreateAsync(entity, transaction);
            }
        }
        else
        {
            result = await connection.CreateAsync(entity, transaction);
        }

        return result;
    }

    public List<T> Read(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        List<T> result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                connection.Open();
                result = connection.Read<T>(id, whereClause, transaction);
            }
        }
        else
        {
            result = connection.Read<T>(id, whereClause, transaction);
        }

        return result;
    }

    public async Task<List<T>> ReadAsync(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        List<T> result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                await connection.OpenAsync();
                result = await connection.ReadAsync<T>(id, whereClause, transaction);
            }
        }
        else
        {
            result = await connection.ReadAsync<T>(id, whereClause, transaction);
        }

        return result;
    }

    public dynamic Update(T entity, bool nullable = false, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                connection.Open();
                result = connection.Update(entity, nullable, whereClause, transaction);
            }
        }
        else
        {
            result = connection.Update(entity, nullable, whereClause, transaction);
        }

        return result;
    }

    public async Task<dynamic> UpdateAsync(T entity, bool nullable = false, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                await connection.OpenAsync();
                result = await connection.UpdateAsync(entity, nullable, whereClause, transaction);
            }
        }
        else
        {
            result = await connection.UpdateAsync(entity, nullable, whereClause, transaction);
        }

        return result;
    }

    public dynamic Delete(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                connection.Open();
                result = connection.Delete<T>(id, whereClause, transaction);
            }
        }
        else
        {
            result = connection.Delete<T>(id, whereClause, transaction);
        }

        return result;
    }

    public async Task<dynamic> DeleteAsync(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null)
    {
        dynamic result;

        if (connection is null)
        {
            using (connection = ConnectionFactory.CreateDbConnection(_connStr))
            {
                await connection.OpenAsync();
                result = await connection.DeleteAsync<T>(id, whereClause, transaction);
            }
        }
        else
        {
            result = await connection.DeleteAsync<T>(id, whereClause, transaction);
        }

        return result;
    }
}
