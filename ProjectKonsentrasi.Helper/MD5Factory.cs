using System.Security.Cryptography;
using System.Text;

namespace ProjectKonsentrasi.Helper;
public class MD5Factory
{
    public static string Generate(string str)
    {
        MD5 md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }
}