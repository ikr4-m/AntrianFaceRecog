using Microsoft.EntityFrameworkCore;

namespace ProjectKonsentrasi.Webserver.Models.Database;
public class DBContext : DbContext
{
    private string GenerateConnectionString()
    {
        string server = Environment.GetEnvironmentVariable("MARIADB_HOST") ?? "localhost";
        string database = Environment.GetEnvironmentVariable("MARIADB_DATABASE") ?? "konsentrasi";
        string username = Environment.GetEnvironmentVariable("MARIADB_USERNAME") ?? "root";
        string password = Environment.GetEnvironmentVariable("MARIADB_PASSWORD") ?? "mariadbroot";
        return $"Server={server};Database={database};User Id={username};Password={password}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        var connString = GenerateConnectionString();
        var serverVersion = ServerVersion.AutoDetect(connString);
        optionBuilder.UseMySql(connectionString: connString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);
    }
}
