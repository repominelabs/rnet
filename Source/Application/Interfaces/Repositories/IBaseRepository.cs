﻿namespace Application.Interfaces.Repositories;

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
    List<T> Get<T>(string whereClause = null);
    Task<List<T>> GetAsync<T>(dynamic id);
    Task<List<T>> GetAsync<T>(string whereClause = null);
    dynamic Update<T>(T entity, bool nullable = false, string whereClause = null);
    Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string whereClause = null);
    List<T> Query<T>();
}
