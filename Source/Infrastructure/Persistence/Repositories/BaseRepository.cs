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
        private IEnumerable<PropertyInfo>? Properties => typeof(T).GetProperties();
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

            using var connection = new NpgsqlConnection(_connectionString);
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
        public dynamic Delete(T entity)
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
        public dynamic Delete(string whereClause)
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
        public async Task<dynamic> DeleteAsync(T entity)
        {
            var sql = $"delete from {TableName} where {PrimaryKey?.Name} = @{PrimaryKey?.Name}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteAsync(string whereClause)
        {
            var sql = $"delete from {TableName} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> Get(dynamic id)
        {
            var sql = $"select * from {TableName} where {PrimaryKey?.Name} = @{id}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            List<T> result = connection.Query<T>(sql).ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public List<T> Get(string whereClause)
        {
            var sql = $"select * from {TableName} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            List<T> result = connection.Query<T>(sql).ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> Get()
        {
            var sql = $"select * from {TableName}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            List<T> result = connection.Query<T>(sql).ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync(dynamic id)
        {
            var sql = $"select * from {TableName} where {PrimaryKey?.Name} = @{id}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql);
            return result.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync(string whereClause)
        {
            var sql = $"select * from {TableName} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql);
            return result.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAsync()
        {
            var sql = $"select * from {TableName}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql);
            return result.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public dynamic Update(T entity, bool nullable = false)
        {
            // Create stringOfSets with entity's not null attribute(s) if nullable is false
            IEnumerable<string>? columns = Properties?.Where(x => x.GetValue(entity, null) != null)?.Select(x => x.Name); 
            string stringOfSets = string.Join(", ", columns?.Select(x => $"{x} = @{x}"));

            // Create stringOfSets with entity's all attribute(s) if nullable is true
            if (nullable)
            {
                stringOfSets = string.Join(", ", Columns.Select(x => $"{x} = @{x}"));
            }

            var sql = $"update {TableName} set {stringOfSets} where {PrimaryKey?.Name} = @{PrimaryKey?.Name}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereClause"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public dynamic Update(T entity, string whereClause, bool nullable = false)
        {
            // Create stringOfSets with entity's not null attribute(s) if nullable is false
            IEnumerable<string>? columns = Properties?.Where(x => x.GetValue(entity, null) != null)?.Select(x => x.Name);
            string stringOfSets = string.Join(", ", columns?.Select(x => $"{x} = @{x}"));

            // Create stringOfSets with entity's all attribute(s) if nullable is true
            if (nullable)
            {
                stringOfSets = string.Join(", ", Columns.Select(x => $"{x} = @{x}"));
            }

            var sql = $"update {TableName} set {stringOfSets} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var result = connection.Execute(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateAsync(T entity, bool nullable = false)
        {
            // Create stringOfSets with entity's not null attribute(s) if nullable is false
            IEnumerable<string>? columns = Properties?.Where(x => x.GetValue(entity, null) != null)?.Select(x => x.Name);
            string stringOfSets = string.Join(", ", columns?.Select(x => $"{x} = @{x}"));

            // Create stringOfSets with entity's all attribute(s) if nullable is true
            if (nullable)
            {
                stringOfSets = string.Join(", ", Columns.Select(x => $"{x} = @{x}"));
            }

            var sql = $"update {TableName} set {stringOfSets} where {PrimaryKey?.Name} = @{PrimaryKey?.Name}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="whereClause"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateAsync(T entity, string whereClause, bool nullable = false)
        {
            // Create stringOfSets with entity's not null attribute(s) if nullable is false
            IEnumerable<string>? columns = Properties?.Where(x => x.GetValue(entity, null) != null)?.Select(x => x.Name);
            string stringOfSets = string.Join(", ", columns?.Select(x => $"{x} = @{x}"));

            // Create stringOfSets with entity's all attribute(s) if nullable is true
            if (nullable)
            {
                stringOfSets = string.Join(", ", Columns.Select(x => $"{x} = @{x}"));
            }

            var sql = $"update {TableName} set {stringOfSets} where {whereClause}";

            using var connection = new NpgsqlConnection(_connectionString);
            _ = connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
}
