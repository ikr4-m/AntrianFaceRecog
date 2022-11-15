using ProjectKonsentrasi.Helper.Extension;
using ProjectKonsentrasi.Webserver.Models.Database;
using ProjectKonsentrasi.Webserver.Models.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class DashboardManageAdminController : Controller
{
    private DBContext _db = new DBContext();
    private const string _DefaultAdminName = "Admin";

    [HttpGet("dashboard/manage_clinic")]
    public async Task<IActionResult> Index([FromQuery] ulong? id)
    {
        return View();
    }

    [HttpPost("dashboard/manage_clinic/add")]
    public async Task<IActionResult> AddData([FromForm] IFormCollection form)
    {
        return View();
    }

    [HttpPost("dashboard/manage_clinic/edit")]
    public async Task<IActionResult> EditData([FromForm] IFormCollection form)
    {
        return View();
    }

    [HttpPost("dashboard/manage_clinic/delete")]
    public async Task<IActionResult> DeleteData([FromForm] IFormCollection form)
    {
        return View();
    }
}
