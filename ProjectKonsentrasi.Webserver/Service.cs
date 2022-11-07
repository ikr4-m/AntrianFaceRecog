using ProjectKonsentrasi.Webserver.Models.Database;

namespace ProjectKonsentrasi.Webserver;
public class Services
{
    public static WebApplication ConfigureServiceBuilder(ref WebApplicationBuilder builder)
    {
        // Connect controllers with views
        builder.Services.AddControllersWithViews();

        // Session service
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(option =>
        {
            option.IdleTimeout = TimeSpan.FromSeconds(10);
            option.Cookie.HttpOnly = true;
            option.Cookie.IsEssential = true;
        });

        // Register singleton
        builder.Services
            .AddSingleton<DBContext>();

        return builder.Build();
    }

    public static void ConfigureDevelopmentRequestPipeline(ref WebApplication app)
    {
        app.UseExceptionHandler("/error");
    }
}