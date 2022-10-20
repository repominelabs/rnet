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

    private IEnumerable<string?> GetColumns<T>() => typeof(T).GetProperties().Where(e => /* e.Name != PrimaryKey.Name &&*/ e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => e.GetCustomAttribute<ColumnAttribute>().Name);

    public dynamic Create<T>(T entity, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    {
        IEnumerable<string?> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name} into :lastcid";

        DynamicParameters parameters = new(entity);
        parameters.Add(name: "lastcid", dbType: dbType, direction: ParameterDirection.Output);

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            var result = connection.Execute(sql, parameters, dbTransaction);
            return parameters.Get<int>("lastcid");
        }
        else 
        {
            var result = dbConnection.Execute(sql, parameters, dbTransaction);
            return parameters.Get<int>("lastcid");
        }
    }

    public async Task<dynamic> CreateAsync<T>(T entity)
    {
        IEnumerable<string?> columns = GetColumns<T>();
        var stringOfColumns = string.Join(", ", columns);
        var stringOfParameters = string.Join(", ", columns.Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name}";

        using var connection = new OracleConnection(_connStr);
        var result = await connection.ExecuteAsync(sql, entity);
        return result;
    }

    public async Task<dynamic> CreateAsync<T>(IDbConnection dbConnection, IDbTransaction dbTransaction, T entity)
    {
        IEnumerable<string?> columns = GetColumns<T>();
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
}
