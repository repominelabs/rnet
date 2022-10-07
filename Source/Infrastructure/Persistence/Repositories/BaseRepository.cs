using Application.Interfaces.Repositories;
using Dapper;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

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
        /// Base Create dapper ops
        /// </summary>
        public dynamic Create(T entity)
        {
            var stringOfColumns = string.Join(", ", Columns);
            var stringOfParameters = string.Join(", ", Columns.Select(e => "@" + e));
            var query = $"insert into {TableName} ({stringOfColumns}) values ({stringOfParameters})";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(query, entity);
            return result;
        }

        /// <summary>
        /// Base CreateAsync dapper ops
        /// </summary>
        public async Task<dynamic> CreateAsync(T entity)
        {
            var stringOfColumns = string.Join(", ", Columns);
            var stringOfParameters = string.Join(", ", Columns.Select(e => "@" + e));
            var query = $"insert into {TableName} ({stringOfColumns}) values ({stringOfParameters})";

            await using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(query, entity);
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

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(string whereClause)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string whereClause)
        {
            throw new NotImplementedException();
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
