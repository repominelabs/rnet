using System.Data;

namespace Application.Interfaces.DatabaseManagers;

public interface IDatabaseManager
{
    dynamic Create<T>(T entity);
    dynamic Create<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity);
    Task<dynamic> CreateAsync<T>(T entity);
    Task<dynamic> CreateAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity);
    dynamic Delete<T>(string whereClause);
    dynamic Delete<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause);
    Task<dynamic> DeleteAsync<T>(string whereClause);
    Task<dynamic> DeleteAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause);
    List<T> Get<T>(string whereClause = null);
    List<T> Get<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause = null);
    Task<List<T>> GetAsync<T>(string whereClause = null);
    Task<List<T>> GetAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause = null);
    List<T> Query<T>(string query);
    List<T> Query<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query);
    List<T> Query<T>(string query, object parameters);
    List<T> Query<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query, object parameters);
    Task<List<T>> QueryAsync<T>(string query);
    Task<List<T>> QueryAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query);
    Task<List<T>> QueryAsync<T>(string query, object parameters);
    Task<List<T>> QueryAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query, object parameters);
    dynamic Update<T>(T entity, bool nullable = false, string whereClause = null);
    dynamic Update<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> UpdateAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity, bool nullable = false, string whereClause = null);
}
