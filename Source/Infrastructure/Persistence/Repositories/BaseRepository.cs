using Application.Interfaces.DatabaseManagers;
using Application.Interfaces.Repositories;
using System.Data;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// 
/// </summary>
public class BaseRepository<T> : IBaseRepository<T>
{
    private readonly string _connStr;
    private readonly IDatabaseManager _databaseManager;

    public BaseRepository(string connStr, IDatabaseManager databaseManager)
    {
        _connStr = connStr;
        _databaseManager = databaseManager;
    }

    public dynamic CreateOrUpdate(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    {
        T? response = _databaseManager.Get<T>(whereClause, dbConnection, dbTransaction).FirstOrDefault();
        if (response == null)
        {
            dynamic result = _databaseManager.Create(entity, dbConnection, dbTransaction, dbType);
            return result;
        }
        else
        {
            dynamic result = _databaseManager.Update(entity, nullable, whereClause, dbConnection, dbTransaction);
            return result;
        }
    }

    public async Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    {
        var response = await _databaseManager.GetAsync<T>(whereClause, dbConnection, dbTransaction);
        if (response == null || (response != null && response.Count == 0))
        {
            dynamic result = await _databaseManager.CreateAsync(entity, dbConnection, dbTransaction, dbType);
            return result;
        }
        else
        {
            dynamic result = await _databaseManager.UpdateAsync(entity, nullable, whereClause, dbConnection, dbTransaction);
            return result;
        }
    }
}
