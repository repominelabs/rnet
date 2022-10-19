namespace Application.Interfaces.Repositories;

public interface IBaseRepository
{
    dynamic Create(T entity);
    Task<dynamic> CreateAsync(T entity);
    dynamic CreateOrUpdate(T entity);
    Task<dynamic> CreateOrUpdateAsync(T entity);
    dynamic Delete(T entity);
    dynamic Delete(string whereClause);
    Task<dynamic> DeleteAsync(T entity);
    Task<dynamic> DeleteAsync(string whereClause);
    List<T> Get(dynamic id);
    List<T> Get(string whereClause);
    List<T> Get();
    Task<List<T>> GetAsync(dynamic id);
    Task<List<T>> GetAsync(string whereClause);
    Task<List<T>> GetAsync();
    dynamic Update(T entity, bool nullable = false);
    dynamic Update(T entity, string whereClause, bool nullable = false);
    Task<dynamic> UpdateAsync(T entity, bool nullable = false);
    Task<dynamic> UpdateAsync(T entity, string whereClause, bool nullable = false);
}
