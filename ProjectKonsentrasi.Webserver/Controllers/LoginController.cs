using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class LoginController : Controller
{
    [HttpGet("login")]
    public IActionResult Index()
    {
        return View();
    }
}