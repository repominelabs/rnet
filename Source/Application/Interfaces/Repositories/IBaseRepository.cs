namespace Application.Interfaces.Repositories;

public interface IBaseRepository
{
    dynamic Create<T>(T entity);
    Task<dynamic> CreateAsync<T>(T entity);
    dynamic CreateOrUpdate<T>(T entity);
    Task<dynamic> CreateOrUpdateAsync<T>(T entity);
    dynamic Delete<T>(dynamic id);
    dynamic Delete<T>(string whereClause);
    Task<dynamic> DeleteAsync<T>(dynamic id);
    Task<dynamic> DeleteAsync<T>(string whereClause);
    List<T> Get<T>(dynamic id);
    List<T> Get<T>(string whereClause);
    List<T> Get<T>();
    Task<List<T>> GetAsync<T>(dynamic id);
    Task<List<T>> GetAsync<T>(string whereClause);
    Task<List<T>> GetAsync<T>();
    dynamic Update<T>(T entity, bool nullable = false);
    dynamic Update<T>(T entity, string whereClause, bool nullable = false);
    Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false);
    Task<dynamic> UpdateAsync<T>(T entity, string whereClause, bool nullable = false);
}
