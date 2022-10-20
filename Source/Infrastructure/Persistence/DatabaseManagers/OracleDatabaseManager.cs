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

    private IEnumerable<string?> GetColumns<T>() => typeof(T).GetProperties().Where(e => e.Name != GetPrimaryKey<T>().Name && e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => e.GetCustomAttribute<ColumnAttribute>()?.Name);

    private IEnumerable<string> GetColumnPropertyNames<T>() => typeof(T).GetProperties().Where(e => e.Name != GetPrimaryKey<T>().Name && e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => e.Name);

    public dynamic Create<T>(T entity, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    {
        var stringOfColumns = string.Join(", ", GetColumns<T>());
        var stringOfParameters = string.Join(", ", GetColumnPropertyNames<T>().Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name} into :lastcid";

        DynamicParameters parameters = new(entity);
        parameters.Add(name: "lastcid", dbType: dbType, direction: ParameterDirection.Output);

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            var result = connection.Execute(sql, parameters, dbTransaction);
            return parameters.Get<dynamic>("lastcid");
        }
        else 
        {
            var result = dbConnection.Execute(sql, parameters, dbTransaction);
            return parameters.Get<dynamic>("lastcid");
        }
    }

    public async Task<dynamic> CreateAsync<T>(T entity, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null, DbType dbType = DbType.Int32)
    {
        var stringOfColumns = string.Join(", ", GetColumns<T>());
        var stringOfParameters = string.Join(", ", GetColumnPropertyNames<T>().Select(e => ":" + e));
        var sql = $"insert into {_schema}.{GetTableName<T>()} ({stringOfColumns}) values ({stringOfParameters}) returning {GetPrimaryKey<T>()?.Name} into :lastcid";

        DynamicParameters parameters = new(entity);
        parameters.Add(name: "lastcid", dbType: dbType, direction: ParameterDirection.Output);

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            var result = await connection.ExecuteAsync(sql, parameters, dbTransaction);
            return parameters.Get<dynamic>("lastcid");
        }
        else
        {
            var result = await dbConnection.ExecuteAsync(sql, parameters, dbTransaction);
            return parameters.Get<dynamic>("lastcid");
        }
    }

    public dynamic Delete<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        string sql;
        if (string.IsNullOrEmpty(whereClause))
        {
            sql = $"delete from {_schema}.{GetTableName<T>()}";
        }
        else
        {
            sql = $"delete from {_schema}.{GetTableName<T>()} where {whereClause}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            int result = connection.Execute(sql, null, dbTransaction);
            return result;
        }
        else
        {
            int result = dbConnection.Execute(sql, null, dbTransaction);
            return result;
        }
    }

    public async Task<dynamic> DeleteAsync<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        string sql;
        if (string.IsNullOrEmpty(whereClause))
        {
            sql = $"delete from {_schema}.{GetTableName<T>()}";
        }
        else
        {
            sql = $"delete from {_schema}.{GetTableName<T>()} where {whereClause}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            int result = await connection.ExecuteAsync(sql, null, dbTransaction);
            return result;
        }
        else
        {
            int result = await dbConnection.ExecuteAsync(sql, null, dbTransaction);
            return result;
        }
    }

    public List<T> Get<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        string sql;
        if (!string.IsNullOrEmpty(whereClause))
        {
            sql = $"select * from {_schema}.{GetTableName<T>()} where {whereClause}";
        }
        else
        {
            sql = $"select * from {_schema}.{GetTableName<T>()}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            List<T> result = connection.Query<T>(sql, null, dbTransaction).ToList();
            return result;
        }
        else
        {
            List<T> result = dbConnection.Query<T>(sql, null, dbTransaction).ToList();
            return result;
        }
    }

    public async Task<List<T>> GetAsync<T>(string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        string sql;
        if (!string.IsNullOrEmpty(whereClause))
        {
            sql = $"select * from {_schema}.{GetTableName<T>()} where {whereClause}";
        }
        else
        {
            sql = $"select * from {_schema}.{GetTableName<T>()}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            IEnumerable<T> result = await connection.QueryAsync<T>(sql, null, dbTransaction);
            return result.ToList();
        }
        else
        {
            IEnumerable<T> result = await dbConnection.QueryAsync<T>(sql, null, dbTransaction);
            return result.ToList();
        }
    }

    public List<T> Query<T>(string query, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            List<T> result = connection.Query<T>(query, null, dbTransaction).ToList();
            return result;
        }
        else
        {
            List<T> result = dbConnection.Query<T>(query, null, dbTransaction).ToList();
            return result;
        }
    }

    public List<T> Query<T>(string query, object parameters, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        foreach (PropertyInfo property in parameters.GetType().GetProperties())
        {
            query = query.Replace($":{property.Name}", $"'{property.GetValue(parameters)}'");
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            List<T> result = connection.Query<T>(query, null, dbTransaction).ToList();
            return result;
        }
        else
        {
            List<T> result = dbConnection.Query<T>(query, null, dbTransaction).ToList();
            return result;
        }
    }

    public async Task<List<T>> QueryAsync<T>(string query, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            IEnumerable<T> result = await connection.QueryAsync<T>(query, null, dbTransaction);
            return result.ToList();
        }
        else
        {
            IEnumerable<T> result = await dbConnection.QueryAsync<T>(query, null, dbTransaction);
            return result.ToList();
        }
    }

    public async Task<List<T>> QueryAsync<T>(string query, object parameters, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        foreach (PropertyInfo property in parameters.GetType().GetProperties())
        {
            query = query.Replace($":{property.Name}", $"'{property.GetValue(parameters)}'");
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            IEnumerable<T> result = await connection.QueryAsync<T>(query, null, dbTransaction);
            return result.ToList();
        }
        else
        {
            IEnumerable<T> result = await dbConnection.QueryAsync<T>(query, null, dbTransaction);
            return result.ToList();
        }
    }

    public dynamic Update<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        // Create stringOfSets with entity's not null attribute(s) if nullable is false
        string stringOfSets;

        // Create stringOfSets with entity's all attribute(s) if nullable is true
        if (nullable)
        {
            stringOfSets = string.Join(", ", GetProperties<T>().Where(e => e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => $"{e.GetCustomAttribute<ColumnAttribute>().Name} = :{e.Name}"));
        }
        else
        {
            string[] propertyNames = entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ColumnAttribute>() != null && x.GetValue(entity) != null).Select(x => x.GetCustomAttribute<ColumnAttribute>().Name).ToArray(); 
            stringOfSets = string.Join(" , ", propertyNames.Select(propertyName => propertyName + " = :" + entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ColumnAttribute>() != null && x.GetCustomAttribute<ColumnAttribute>().Name == propertyName).Select(e => e.Name).FirstOrDefault()));
        }

        string sql;
        if (!string.IsNullOrEmpty(whereClause))
        {
            sql = $"update {_schema}.{GetTableName<T>()} set {stringOfSets} where {whereClause}";
        }
        else
        {
            sql = $"update {_schema}.{GetTableName<T>()} set {stringOfSets} where {GetPrimaryKey<T>()? .GetCustomAttribute<ColumnAttribute>()?.Name} = :{GetPrimaryKey<T>()?.Name}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            var result = connection.Execute(sql, entity, dbTransaction);
            return result;
        }
        else
        {
            var result =  dbConnection.Execute(sql, entity, dbTransaction);
            return result;
        }
    }

    public async Task<dynamic> UpdateAsync<T>(T entity, bool nullable = false, string? whereClause = null, IDbConnection? dbConnection = null, IDbTransaction? dbTransaction = null)
    {
        // Create stringOfSets with entity's not null attribute(s) if nullable is false
        string stringOfSets;

        // Create stringOfSets with entity's all attribute(s) if nullable is true
        if (nullable)
        {
            stringOfSets = string.Join(", ", GetProperties<T>().Where(e => e.GetCustomAttribute<ColumnAttribute>() != null).Select(e => $"{e.GetCustomAttribute<ColumnAttribute>().Name} = :{e.Name}"));
        }
        else
        {
            string[] propertyNames = entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ColumnAttribute>() != null && x.GetValue(entity) != null).Select(x => x.GetCustomAttribute<ColumnAttribute>().Name).ToArray();
            stringOfSets = string.Join(" , ", propertyNames.Select(propertyName => propertyName + " = :" + entity.GetType().GetProperties().Where(x => x.GetCustomAttribute<ColumnAttribute>() != null && x.GetCustomAttribute<ColumnAttribute>().Name == propertyName).Select(e => e.Name).FirstOrDefault()));
        }

        string sql;
        if (!string.IsNullOrEmpty(whereClause))
        {
            sql = $"update {_schema}.{GetTableName<T>()} set {stringOfSets} where {whereClause}";
        }
        else
        {
            sql = $"update {_schema}.{GetTableName<T>()} set {stringOfSets} where {GetPrimaryKey<T>()?.GetCustomAttribute<ColumnAttribute>()?.Name} = :{GetPrimaryKey<T>()?.Name}";
        }

        if (dbConnection == null)
        {
            using var connection = new OracleConnection(_connStr);
            var result = await connection.ExecuteAsync(sql, entity, dbTransaction);
            return result;
        }
        else
        {
            var result = await dbConnection.ExecuteAsync(sql, entity, dbTransaction);
            return result;
        }
    }
}
