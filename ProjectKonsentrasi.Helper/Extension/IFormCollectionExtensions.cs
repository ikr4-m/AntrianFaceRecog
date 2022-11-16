using Microsoft.AspNetCore.Http;

namespace ProjectKonsentrasi.Helper.Extension;
public static class IFormCollectionExtensions
{
    public static string Get(this IFormCollection form, string key)
    {
        return form[key][0] ?? "";
    }
}
