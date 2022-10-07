using Application.Interfaces.Repositories;
using Dapper;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static Dapper.SqlMapper;

namespace Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string TableName => typeof(T).GetCustomAttribute<TableAttribute>().Name;
        private PropertyInfo? PrimaryKey => typeof(T).GetProperties().Where(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(KeyAttribute))).FirstOrDefault();
        private IEnumerable<string> Columns => typeof(T).GetProperties().Where(e => /* e.Name != PrimaryKey.Name &&*/ e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => e.GetCustomAttribute<ColumnAttribute>().Name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public dynamic Create(T entity)
        {
            var stringOfColumns = string.Join(", ", Columns);
            var stringOfParameters = string.Join(", ", Columns.Select(e => "@" + e));
            var sql = $"insert into {TableName} ({stringOfColumns}) values ({stringOfParameters}) returning {PrimaryKey?.Name}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateAsync(T entity)
        {
            var stringOfColumns = string.Join(", ", Columns);
            var stringOfParameters = string.Join(", ", Columns.Select(e => "@" + e));
            var sql = $"insert into {TableName} ({stringOfColumns}) values ({stringOfParameters}) returning {PrimaryKey?.Name}";

            await using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }

        public dynamic CreateOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> CreateOrUpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            var sql = $"delete from {TableName} where {PrimaryKey?.Name} = @{PrimaryKey?.Name}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public int Delete(string whereClause)
        {
            var sql = $"delete from {TableName} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(sql);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(T entity)
        {
            var sql = $"delete from {TableName} where {PrimaryKey?.Name} = @{PrimaryKey?.Name}";

            await using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string whereClause)
        {
            var sql = $"delete from {TableName} where {whereClause}";

            await using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        public List<T> Get(dynamic id)
        {
            throw new NotImplementedException();
        }

        public List<T> Get(string whereClause)
        {
            throw new NotImplementedException();
        }

        public List<T> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsync(dynamic id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsync(string whereClause)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
