using ProjectKonsentrasi.Helper;
using Microsoft.EntityFrameworkCore;

namespace ProjectKonsentrasi.Webserver.Models.Database;
public class DBContext : DbContext
{
    public DbSet<AdminUser> AdminUser => Set<AdminUser>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        var connString = ConnectionString.Generate();
        var serverVersion = ServerVersion.AutoDetect(connString);
        optionBuilder.UseMySql(connectionString: connString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);
    }
}
