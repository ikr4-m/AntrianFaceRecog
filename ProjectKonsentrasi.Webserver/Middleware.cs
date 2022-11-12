using ProjectKonsentrasi.Helper.Extension;

namespace ProjectKonsentrasi.Webserver;
public class Middleware
{
    public static void ConfigureMiddleware(ref WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSession();
        app.MapRazorPages();

        // Dashboard middleware
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.HasContainRoute("dashboard") && context.Session.Get("Login") == null)
            {
                context.Response.Redirect("/login");
            }
            else
            {
                await next(context);
            }
        });
    }
}