using System.Data;

namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    dynamic CreateOrUpdate(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32);
    Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string whereClause = null, IDbConnection dbConnection = null, IDbTransaction dbTransaction = null, DbType dbType = DbType.Int32);
}
