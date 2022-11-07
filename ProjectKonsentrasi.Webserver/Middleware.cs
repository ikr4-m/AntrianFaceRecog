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
    }
}