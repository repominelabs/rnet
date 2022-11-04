using Application.Interfaces.Repositories;
using Cinis.PostgreSql;
using Domain.Entities;
using Npgsql;
using System.Data;

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
            int id = connection.Create(entity);
            return id;
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
            int id = await connection.CreateAsync(entity);
            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public dynamic CreateOrUpdate(T entity, bool nullable = false, string? whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string? whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32)
    {
        throw new NotImplementedException();
    }

    public dynamic Delete(dynamic? id = null, string? whereClause = null)
    {
        throw new NotImplementedException();
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

    //public dynamic CreateOrUpdate(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    //{
    //    T? response = _databaseManager.Get<T>(whereClause, dbConnection, dbTransaction).FirstOrDefault();
    //    if (response == null)
    //    {
    //        dynamic result = _databaseManager.Create(entity, dbConnection, dbTransaction, dbType);
    //        return result;
    //    }
    //    else
    //    {
    //        dynamic result = _databaseManager.Update(entity, nullable, whereClause, dbConnection, dbTransaction);
    //        return result;
    //    }
    //}

    //public async Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    //{
    //    var response = await _databaseManager.GetAsync<T>(whereClause, dbConnection, dbTransaction);
    //    if (response == null || (response != null && response.Count == 0))
    //    {
    //        dynamic result = await _databaseManager.CreateAsync(entity, dbConnection, dbTransaction, dbType);
    //        return result;
    //    }
    //    else
    //    {
    //        dynamic result = await _databaseManager.UpdateAsync(entity, nullable, whereClause, dbConnection, dbTransaction);
    //        return result;
    //    }
    //}
}
