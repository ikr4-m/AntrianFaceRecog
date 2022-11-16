using ProjectKonsentrasi.Helper;
using ProjectKonsentrasi.Helper.Extension;
using ProjectKonsentrasi.Webserver.Models.Database;
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
        ViewData["Data"] = await _db.AdminUser.Where(x => x.Nama != _DefaultAdminName).ToListAsync();
        ViewData["DataModified"] = await _db.AdminUser.Where(x => x.ID == id).FirstOrDefaultAsync();
        ViewBag.IsCRUD = id != null;
        return View();
    }

    [HttpPost("dashboard/manage_admin/add")]
    public async Task<IActionResult> AddData([FromForm] IFormCollection form)
    {
        if (form.Get("Nama") == _DefaultAdminName)
        {
            TempData["Message"] = "Admin utama tidak boleh ada yang kembar!";
            return View();
        }

        await _db.AdminUser.AddAsync(new AdminUser
        {
            Nama = form.Get("Nama"),
            Email = form.Get("Email"),
            Password = MD5Factory.Generate(form.Get("Password"))
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah ditambahkan!";
        return View();
    }

    [HttpPost("dashboard/manage_admin/edit")]
    public async Task<IActionResult> EditData([FromForm] IFormCollection form)
    {
        _db.AdminUser.Update(new AdminUser
        {
            ID = ulong.Parse(form.Get("ID") ?? "0"),
            Nama = form.Get("Nama"),
            Email = form.Get("Email"),
            Password = MD5Factory.Generate(form.Get("Password"))
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah diubah!";
        return RedirectToAction("Index", this);
    }

    [HttpPost("dashboard/manage_admin/delete")]
    public async Task<IActionResult> DeleteData([FromForm] IFormCollection form)
    {
        var data = await _db.AdminUser
            .Where(x => x.ID == ulong.Parse(form["ID"][0]))
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
