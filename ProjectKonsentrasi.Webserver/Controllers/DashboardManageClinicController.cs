using ProjectKonsentrasi.Helper.Extension;
using ProjectKonsentrasi.Webserver.Models.Database;
using ProjectKonsentrasi.Webserver.Models.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class DashboardManageClinicController : Controller
{
    private DBContext _db = new DBContext();

    [HttpGet("dashboard/manage_clinic")]
    public async Task<IActionResult> Index()
    {
        ViewData["User"] = HttpContext.Session.Get<AuthCookie>("Login")!.Nama;
        ViewData["Klinik"] = await _db.ListKlinikTujuan.ToListAsync();
        return View();
    }
}
