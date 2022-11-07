using ProjectKonsentrasi.Webserver.Extension;
using ProjectKonsentrasi.Webserver.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
[ApiController]
public class AuthController : Controller
{
    private class LoginStructure
    {
        public string Email;
        public string Password;

        public LoginStructure(IFormCollection form)
        {
            Email = form["email"][0];
            Password = form["password"][0];
        }
    }

    [HttpPost("auth/login")]
    public ActionResult Login([FromForm] IFormCollection form)
    {
        var data = new LoginStructure(form);
        if (data.Email != "admin" && data.Password != "admin")
        {
            HttpContext.Response.StatusCode = 500;
            return Json(new { Code = 500, Message = "Password Salah!" });
        }

        HttpContext.Session.Set<AuthCookie>("Login", new AuthCookie
        {
            ID = 1,
            Nama = data.Email
        });

        return Json(new { Code = 200, Message = "Password benar, anda telah login!" });
    }


    [HttpGet("auth/logout")]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
}
