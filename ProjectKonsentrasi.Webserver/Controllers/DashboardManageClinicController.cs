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
    public async Task<IActionResult> Index([FromQuery] ulong? id)
    {
        ViewData["User"] = HttpContext.Session.Get<AuthCookie>("Login")!.Nama;
        ViewData["Data"] = await _db.ListKlinikTujuan.ToListAsync();
        ViewData["DataModified"] = await _db.ListKlinikTujuan.Where(x => x.ID == id).FirstOrDefaultAsync();
        ViewBag.IsCRUD = id != null;
        return View();
    }

    [HttpPost("dashboard/manage_clinic/add")]
    public async Task<IActionResult> AddData([FromForm] IFormCollection form)
    {
        await _db.ListKlinikTujuan.AddAsync(new ListKlinikTujuan
        {
            NamaKlinik = form["NamaKlinik"][0]
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah ditambahkan!";
        return RedirectToAction("Index", this);
    }

    [HttpPost("dashboard/manage_clinic/edit")]
    public async Task<IActionResult> EditData([FromForm] IFormCollection form)
    {
        _db.ListKlinikTujuan.Update(new ListKlinikTujuan
        {
            ID = ulong.Parse(form["ID"][0] ?? "0"),
            NamaKlinik = form["NamaKlinik"][0]
        });
        await _db.SaveChangesAsync();

        TempData["Message"] = "Data telah diubah!";
        return RedirectToAction("Index", this);
    }

    [HttpPost("dashboard/manage_clinic/delete")]
    public async Task<IActionResult> DeleteData([FromForm] IFormCollection form)
    {
        ulong id = ulong.Parse(form["ID"][0] ?? "0");
        var data = await _db.ListKlinikTujuan.Where(x => x.ID == id).FirstOrDefaultAsync();
        if (data == null)
        {
            return StatusCode(404, new { Message = "Not found" });
        }
        _db.ListKlinikTujuan.Remove(data);
        await _db.SaveChangesAsync();

        return Json(new { Message = "Success!" });
    }
}
