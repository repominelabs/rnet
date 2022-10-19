namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    dynamic Create(T entity);
    Task<dynamic> CreateAsync(T entity);
    dynamic CreateOrUpdate(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string whereClause = null);
    dynamic Delete(dynamic id);
    dynamic Delete(string whereClause);
    Task<dynamic> DeleteAsync(dynamic id);
    Task<dynamic> DeleteAsync(string whereClause);
    List<T> Get(dynamic id);
    List<T> Get(string whereClause = null);
    Task<List<T>> GetAsync(dynamic id);
    Task<List<T>> GetAsync(string whereClause = null);
    List<T> Query();
    Task<List<T>> QueryAsync();
    dynamic Update(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> UpdateAsync(T entity, bool nullable = false, string whereClause = null);
}
