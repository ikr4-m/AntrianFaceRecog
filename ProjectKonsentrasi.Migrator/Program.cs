using ProjectKonsentrasi.Migrator.List;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ProjectKonsentrasi.Helper;

namespace ProjectKonsentrasi.Migrator;
public class Program
{
    static void Main(string[] args)
    {
        var service = CreateService();
        using var scope = service.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        switch (args[0])
        {
            case "--up": runner.MigrateUp(); break;
            case "--down": runner.MigrateDown(long.Parse(args[1] ?? "0")); break;
        }
    }

    public static IServiceProvider CreateService()
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(ConnectionString.Generate())
                .ScanIn(typeof(_20221104_BaseDatabase).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }
}
