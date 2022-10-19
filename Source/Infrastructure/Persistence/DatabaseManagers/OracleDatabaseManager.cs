using Application.Interfaces.DatabaseManagers;
using System.Data;

namespace Infrastructure.Persistence.DatabaseManagers;

public class OracleDatabaseManager : IOracleDatabaseManager
{
    public dynamic Create<T>(T entity, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> CreateAsync<T>(T entity, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public dynamic CreateOrUpdate<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> CreateOrUpdateAsync<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Delete<T>(dynamic id, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Delete<T>(string whereClause, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> DeleteAsync<T>(dynamic id, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> DeleteAsync<T>(string whereClause, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Get<T>(dynamic id, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Get<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAsync<T>(dynamic id, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAsync<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(string query, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(string query, object parameters, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(string query, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(string query, object parameters, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Update<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        throw new NotImplementedException();
    }
}
