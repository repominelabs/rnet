using System.Data;

namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    // Sync Func(s) 
    dynamic Create(T entity);
    dynamic CreateOrUpdate(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32);
    List<T> Read(dynamic id = null, string whereClause = null);
    dynamic Update(T entity, bool nullable = false, string? whereClause = null);
    dynamic Delete(dynamic id = null, string whereClause = null);

    // Async Func(s)
    Task<dynamic> CreateAsync(T entity);
    Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32);
    Task<List<T>> ReadAsync(dynamic id = null, string whereClause = null);
    Task<dynamic> UpdateAsync(T entity, bool nullable = false, string? whereClause = null);
    Task<dynamic> DeleteAsync(dynamic id = null, string whereClause = null);
}
