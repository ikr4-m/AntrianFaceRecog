using ProjectKonsentrasi.Helper.Extension;
using ProjectKonsentrasi.Webserver.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class DashboardController : Controller
{
    private bool IsLogin() =>
        HttpContext.Session.Get("Login") != null;
    
    [HttpGet("dashboard")]
    public IActionResult Index()
    {
        if (!IsLogin()) return RedirectToAction("Index", "Login");
        ViewData["User"] = HttpContext.Session.Get<AuthCookie>("Login")!.Nama;
        return View();
    }
}
