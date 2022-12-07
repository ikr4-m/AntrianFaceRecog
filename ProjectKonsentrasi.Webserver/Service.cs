using ProjectKonsentrasi.Webserver.Models.Database;
using Microsoft.AspNetCore.SignalR;

namespace ProjectKonsentrasi.Webserver;
public class Services
{
    public static WebApplication ConfigureServiceBuilder(ref WebApplicationBuilder builder)
    {
        // Connect controllers with views
        builder.Services.AddControllersWithViews();

        // Enable razor views
        builder.Services.AddRazorPages();

        // Add SignalR service
        builder.Services.AddSignalR();

        // Session service
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(option =>
        {
            option.IdleTimeout = TimeSpan.FromDays(30);
            option.Cookie.Name = ".Konsentrasi.Session";
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