using ProjectKonsentrasi.Migrator.List;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectKonsentrasi.Migrator;
public class Program
{
    static void Main(string[] args)
    {
        var service = CreateService();
    }

    public static string GenerateConnectionString()
    {
        string server = Environment.GetEnvironmentVariable("MARIADB_HOST") ?? "localhost";
        string database = Environment.GetEnvironmentVariable("MARIADB_DATABASE") ?? "konsentrasi";
        string username = Environment.GetEnvironmentVariable("MARIADB_USERNAME") ?? "root";
        string password = Environment.GetEnvironmentVariable("MARIADB_PASSWORD") ?? "mariadbroot";
        return $"Server={server};Database={database};User Id={username};Password={password}";
    }

    public static IServiceProvider CreateService()
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(GenerateConnectionString())
                .ScanIn(typeof(_20221104_BaseDatabase).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }
}
