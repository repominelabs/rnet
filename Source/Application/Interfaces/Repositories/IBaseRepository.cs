namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    dynamic Create(T entity);
    Task<dynamic> CreateAsync(T entity);
    dynamic CreateOrUpdate(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> CreateOrUpdateAsync(T entity, bool nullable = false, string whereClause = null);
    dynamic Delete(dynamic id = null, string whereClause = null);
    Task<dynamic> DeleteAsync(dynamic id = null, string whereClause = null);
    List<T> Read(dynamic id = null, string whereClause = null);
    Task<List<T>> ReadAsync(dynamic id = null, string whereClause = null);
    dynamic Update(T entity, bool nullable = false, string? whereClause = null);
    Task<dynamic> UpdateAsync(T entity, bool nullable = false, string? whereClause = null);
}
