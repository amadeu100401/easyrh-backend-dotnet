using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace EasyRh.Infra.DataAccess.Migrations;

public static class DataBaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider services)
    {
        EnsureDbCreated(connectionString);
        RunMigartion(services);
    }

    private static void EnsureDbCreated(string connectionString)
    {
        var connectionStringBuilder = BuildConnectionString(connectionString);
        var dbName = connectionStringBuilder.Database;

        connectionStringBuilder.Remove("DataBase");

        using var dbConnection = GetDbConnection(connectionStringBuilder.ConnectionString);

        if (!ExistisSchema(dbConnection, dbName))
        {
            CreateDataBase(dbConnection, dbName);
        }
    }

    private static MySqlConnectionStringBuilder BuildConnectionString(string connectionString) => new MySqlConnectionStringBuilder(connectionString);
   
    private static bool ExistisSchema(MySqlConnection dbConnection, string dbName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("dbName", dbName);

        var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @dbName", parameters);
        return records.Any();
    }

    private static MySqlConnection GetDbConnection(string connectionString) => new MySqlConnection(connectionString);

    private static void CreateDataBase(MySqlConnection dbConnection, string dbName) => dbConnection.Execute($"CREATE DATABASE {dbName};");

    private static void RunMigartion(IServiceProvider services)
    {
        var runner = services.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp(); 
    }
}
