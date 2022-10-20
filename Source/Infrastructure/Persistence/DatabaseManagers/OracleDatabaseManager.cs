using Application.Interfaces.DatabaseManagers;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace Infrastructure.Persistence.DatabaseManagers;

public class OracleDatabaseManager : IOracleDatabaseManager
{
    private readonly string _connStr;
    private readonly string _schema;

    public OracleDatabaseManager(string connStr, string schema)
    {
        _connStr = connStr;
        _schema = schema;
    }

    private string GetTableName<T>() => typeof(T).GetCustomAttribute<TableAttribute>().Name;

    private IEnumerable<PropertyInfo> GetProperties<T>() => typeof(T).GetProperties();

    private PropertyInfo? GetPrimaryKey<T>() => typeof(T).GetProperties().Where(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(KeyAttribute))).FirstOrDefault();

    private IEnumerable<string> GetColumns<T>() => typeof(T).GetProperties().Where(e => /* e.Name != PrimaryKey.Name &&*/ e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => e.GetCustomAttribute<ColumnAttribute>().Name);

    public dynamic Create<T>(T entity)
    {
        IEnumerable<string> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name}";

        using var connection = new OracleConnection(_connStr);
        var result = connection.Execute(sql, entity);
        return result;
    }

    public dynamic Create<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity)
    {
        IEnumerable<string> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name}";

        var result = dbConnection.Execute(sql, entity, dbTransaction);
        return result;
    }

    public async Task<dynamic> CreateAsync<T>(T entity)
    {
        IEnumerable<string> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name}";

        using var connection = new OracleConnection(_connStr);
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }

    public async Task<dynamic> CreateAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity)
    {
        IEnumerable<string> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name}";

        var result = await dbConnection.ExecuteAsync(sql, entity, dbTransaction);
        return result;
    }

    public dynamic Delete<T>(string whereClause)
    {
        var sql = $"delete from {GetTableName<T>()} where 1 = 1 and {whereClause}";

        using var connection = new OracleConnection(_connStr);
        var result = connection.Execute(sql);
        return result;
    }

    public dynamic Delete<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause)
    {
        var sql = $"delete from {GetTableName<T>()} where 1 = 1 and {whereClause}";

        var result = dbConnection.Execute(sql, dbTransaction);
        return result;
    }

    public async Task<dynamic> DeleteAsync<T>(string whereClause)
    {
        string sql = $"delete from {GetTableName<T>()} where 1 = 1 and {whereClause}";

        using var connection = new OracleConnection(_connStr);
        var result = await connection.ExecuteAsync(sql);
        return result;
    }

    public async Task<dynamic> DeleteAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause)
    {
        string sql = $"delete from {GetTableName<T>()} where 1 = 1 and {whereClause}";

        var result = await dbConnection.ExecuteAsync(sql, null, dbTransaction);
        return result;
    }

    public List<T> Get<T>(string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Get<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAsync<T>(string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(string query)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(string query, object parameters)
    {
        throw new NotImplementedException();
    }

    public List<T> Query<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query, object parameters)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(string query)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(string query, object parameters)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> QueryAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, string query, object parameters)
    {
        throw new NotImplementedException();
    }

    public dynamic Update<T>(T entity, bool nullable = false, string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public dynamic Update<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity, bool nullable = false, string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string whereClause = null)
    {
        throw new NotImplementedException();
    }

    public Task<dynamic> UpdateAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity, bool nullable = false, string whereClause = null)
    {
        throw new NotImplementedException();
    }
}
