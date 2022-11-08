using ProjectKonsentrasi.Helper;
using ProjectKonsentrasi.Webserver.Models.Database;
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
        return View();
    }
}