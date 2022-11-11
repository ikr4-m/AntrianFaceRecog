using Microsoft.AspNetCore.Http;

namespace ProjectKonsentrasi.Helper.Extension;
public static class PathStringExtensions
{
    public static bool HasContainRoute(this PathString path, string route)
    {
        return path.ToString().Split('/').Where(x => x == route).Count() > 0;
    }
}
