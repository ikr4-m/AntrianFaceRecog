namespace ProjectKonsentrasi.Webserver;
public class Program
{
    static void Main(string[] args) => new Program().Start(args);
    public void Start(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        var app = Services.ConfigureServiceBuilder(ref builder);

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment()) Services.ConfigureDevelopmentRequestPipeline(ref app);

        // Set middleware
        Middleware.ConfigureMiddleware(ref app);

        // Run app
        app.Run();
    }
}
