using Npgsql;
using System.Data;

namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    dynamic Create(T entity, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    Task<dynamic> CreateAsync(T entity, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    List<T> Read(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    Task<List<T>> ReadAsync(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    dynamic Update(T entity, bool nullable = false, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    Task<dynamic> UpdateAsync(T entity, bool nullable = false, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    dynamic Delete(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
    Task<dynamic> DeleteAsync(object id = null, string whereClause = null, NpgsqlConnection connection = null, NpgsqlTransaction transaction = null);
}
