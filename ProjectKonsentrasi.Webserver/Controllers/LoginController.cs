using ProjectKonsentrasi.Helper;
using ProjectKonsentrasi.Webserver.Models.Database;
using ProjectKonsentrasi.Webserver.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class LoginController : Controller
{
    private DBContext _db = new DBContext();
    private async Task<bool> IsAdminAvailable() =>
        await _db.AdminUser.Where(x => x.ID == 1 || x.Nama == "Admin").FirstOrDefaultAsync() != null;
    private bool IsLogin() =>
        HttpContext.Session.Get("Login") != null;
    
    [HttpGet("login")]
    public async Task<IActionResult> Index()
    {
        if (!await IsAdminAvailable()) return RedirectToAction("SetAdmin", "Login");
        if (IsLogin()) return Json(new { Message = "Anda telah login" });
        return View();
    }

    [HttpGet("login/set-admin")]
    public async Task<IActionResult> SetAdmin()
    {
        if (await IsAdminAvailable()) return RedirectToAction("Index", "Login");
        return View();
    }

    [HttpPost("login/set-admin")]
    public async Task<ActionResult> RegisterAdmin([FromForm] IFormCollection form)
    {
        var data = new LoginFormStructure(form);
        var password = MD5Factory.Generate(data.Password);
        await _db.AdminUser.AddAsync(new AdminUser
        {
            Nama = "Admin",
            Email = data.Email,
            Password = password
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Admin berhasil dibuat, silahkan login kembali";
        return RedirectToAction("Index", "Login");
    }
}