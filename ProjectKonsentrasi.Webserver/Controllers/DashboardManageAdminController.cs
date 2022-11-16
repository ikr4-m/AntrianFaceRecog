using ProjectKonsentrasi.Helper;
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

    [HttpGet("dashboard/manage_admin")]
    public async Task<IActionResult> Index([FromQuery] ulong? id)
    {
        var idSession = HttpContext.Session.Get<AuthCookie>("Login")!.ID;
        ViewData["Data"] = await _db.AdminUser.Where(x => x.Nama != _DefaultAdminName && x.ID != idSession).ToListAsync();
        ViewData["DataModified"] = await _db.AdminUser.Where(x => x.ID == id).FirstOrDefaultAsync();
        ViewBag.IsCRUD = id != null;
        return View();
    }

    [HttpPost("dashboard/manage_admin/add")]
    public async Task<IActionResult> AddData([FromForm] IFormCollection form)
    {
        var ifEmailExist = await _db.AdminUser.Where(x => x.Email == form.Get("Email")).FirstOrDefaultAsync() != null;
        if (ifEmailExist)
        {
            TempData["Message"] = "Email tidak boleh kembar!";
            return RedirectToAction("Index", this);
        }
        if (form.Get("Nama") == _DefaultAdminName)
        {
            TempData["Message"] = "Admin utama tidak boleh ada yang kembar!";
            return RedirectToAction("Index", this);
        }

        await _db.AdminUser.AddAsync(new AdminUser
        {
            Nama = form.Get("Nama"),
            Email = form.Get("Email"),
            Password = MD5Factory.Generate(form.Get("Password"))
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah ditambahkan!";
        return RedirectToAction("Index", this);
    }

    [HttpPost("dashboard/manage_admin/edit")]
    public async Task<IActionResult> EditData([FromForm] IFormCollection form)
    {
        var data = new AdminUser
        {
            ID = ulong.Parse(form.Get("ID") ?? "0"),
            Nama = form.Get("Nama"),
            Email = form.Get("Email"),
        };
        var password = form.Get("Password");
        if (password != "") data.Password = MD5Factory.Generate(password);

        _db.AdminUser.Update(data);
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah diubah!";
        return RedirectToAction("Index", this);
    }

    [HttpPost("dashboard/manage_admin/delete")]
    public async Task<IActionResult> DeleteData([FromForm] IFormCollection form)
    {
        var data = await _db.AdminUser
            .Where(x => x.ID == ulong.Parse(form.Get("ID")))
            .FirstOrDefaultAsync();
        if (data == null)
        {
            return StatusCode(404, new { Message = "Not found" });
        }
        if (data.Nama == _DefaultAdminName)
        {
            return StatusCode(404, new { Message = "Admin utama tidak boleh dihapus!" });
        }
        _db.AdminUser.Remove(data);
        await _db.SaveChangesAsync();
        
        return Json(new { Message = "Success!" });
    }
}
