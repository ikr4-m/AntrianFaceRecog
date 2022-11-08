using ProjectKonsentrasi.Webserver.Models.Database;
using ProjectKonsentrasi.Webserver.Models.View;
using ProjectKonsentrasi.Helper;
using ProjectKonsentrasi.Helper.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectKonsentrasi.Webserver.Controllers;
[ApiController]
public class AuthController : Controller
{
    private DBContext _db = new DBContext();

    [HttpPost("auth/login")]
    public async Task<ActionResult> Login([FromForm] IFormCollection form)
    {
        var parseForm = new LoginFormStructure(form);
        var password = MD5Factory.Generate(parseForm.Password);
        var data = await _db.AdminUser.Where(x => x.Email == parseForm.Email && x.Password == password).FirstOrDefaultAsync();
        if (data == null)
        {
            TempData["Message"] = "Password salah, silahkan ulangi lagi";
            return RedirectToAction("Index", "Login");
        }

        HttpContext.Session.Set<AuthCookie>("Login", new AuthCookie
        {
            ID = data.ID,
            Nama = data.Nama
        });
        return RedirectToAction("Index", "Dashboard");
    }


    [HttpGet("auth/logout")]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
}
