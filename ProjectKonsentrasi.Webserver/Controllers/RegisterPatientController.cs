using ProjectKonsentrasi.Webserver.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class RegisterPatientController : Controller
{
    private DBContext _db = new DBContext();
    
    [HttpGet("register_patient")]
    public IActionResult Index()
    {
        return View();
    }
}