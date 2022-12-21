using ProjectKonsentrasi.Webserver.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class RegisterPatientController : Controller
{
    private DBContext _db = new DBContext();

    [HttpGet("register_patient")]
    public IActionResult Index([FromQuery] bool? withFaker)
    {
        // Buat faker
        ViewBag.WithFaker = withFaker ?? false;
        return View();
    }

    [HttpPost("register_patient/take_picture")]
    public IActionResult TakePicture([FromForm] IFormCollection form)
    {
        return Json("Reserved");
    }
}