namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    object Create(T entity);
    Task<object> CreateAsync(T entity);
    object CreateOrUpdate(T entity, bool nullable = false, object? id = null, string? whereClause = null);
    Task<object> CreateOrUpdateAsync(T entity, bool nullable = false, object? id = null, string? whereClause = null);
    object Delete(object id = null, string whereClause = null);
    Task<object> DeleteAsync(object id = null, string whereClause = null);
    List<T> Read(object id = null, string whereClause = null);
    Task<List<T>> ReadAsync(object id = null, string whereClause = null);
    object Update(T entity, bool nullable = false, string? whereClause = null);
    Task<object> UpdateAsync(T entity, bool nullable = false, string? whereClause = null);
}
