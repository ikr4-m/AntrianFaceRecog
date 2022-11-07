namespace ProjectKonsentrasi.Helper;
public class ConnectionString
{
    public static string Generate()
    {
        string server = Environment.GetEnvironmentVariable("MARIADB_HOST") ?? "localhost";
        string database = Environment.GetEnvironmentVariable("MARIADB_DATABASE") ?? "konsentrasi";
        string username = Environment.GetEnvironmentVariable("MARIADB_USERNAME") ?? "root";
        string password = Environment.GetEnvironmentVariable("MARIADB_PASSWORD") ?? "mariadbroot";
        return $"Server={server};Database={database};User Id={username};Password={password}";
    }
}
