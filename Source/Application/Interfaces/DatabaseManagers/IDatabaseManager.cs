using System.Data;

namespace Application.Interfaces.DatabaseManagers;

public interface IDatabaseManager
{
    dynamic Create<T>(T entity, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<dynamic> CreateAsync<T>(T entity, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    dynamic Delete<T>(string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<dynamic> DeleteAsync<T>(string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    List<T> Get<T>(string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<List<T>> GetAsync<T>(string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    List<T> Query<T>(string query, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    List<T> Query<T>(string query, object parameters, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<List<T>> QueryAsync<T>(string query, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<List<T>> QueryAsync<T>(string query, object parameters, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    dynamic Update<T>(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
    Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null);
}
