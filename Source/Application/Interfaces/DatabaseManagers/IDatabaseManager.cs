namespace Application.Interfaces.DatabaseManagers;

public interface IDatabaseManager
{
    dynamic Create<T>(T entity);
    Task<dynamic> CreateAsync<T>(T entity);
    dynamic CreateOrUpdate<T>(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> CreateOrUpdateAsync<T>(T entity, bool nullable = false, string whereClause = null);
    dynamic Delete<T>(dynamic id);
    dynamic Delete<T>(string whereClause);
    Task<dynamic> DeleteAsync<T>(dynamic id);
    Task<dynamic> DeleteAsync<T>(string whereClause);
    List<T> Get<T>(dynamic id);
    List<T> Get<T>(string whereClause = null);
    Task<List<T>> GetAsync<T>(dynamic id);
    Task<List<T>> GetAsync<T>(string whereClause = null);
    List<T> Query<T>();
    Task<List<T>> QueryAsync<T>();
    dynamic Update<T>(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string whereClause = null);
}
