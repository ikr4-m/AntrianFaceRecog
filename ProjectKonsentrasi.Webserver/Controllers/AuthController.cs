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
        var message = "Password salah!";
        var code = 500;
        if (data.Email == "admin" && data.Password == "admin")
        {
            message = "Passowrd benar!";
            code = 200;
        }

        HttpContext.Response.StatusCode = code;
        return Json(new { Code = code, Message = message });
    }


    [HttpPost("auth/logout")]
    public IActionResult Logout()
    {
        return View();
    }
}
