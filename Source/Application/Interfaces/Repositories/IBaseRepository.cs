namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        dynamic Create(T entity);
        Task<dynamic> CreateAsync(T entity);
        dynamic CreateOrUpdate(T entity);
        Task<dynamic> CreateOrUpdateAsync(T entity);
        int Delete(T entity);
        int Delete(string whereClause);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(string whereClause);
        List<T> Get(dynamic id);
        List<T> Get(string whereClause);
        List<T> Get();
        Task<List<T>> GetAsync(dynamic id);
        Task<List<T>> GetAsync(string whereClause);
        Task<List<T>> GetAsync();
    }
}
