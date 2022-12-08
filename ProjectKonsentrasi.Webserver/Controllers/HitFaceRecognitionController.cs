using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class HitFaceRecognitionController : Controller
{
    [HttpGet("facereg_endpoint")]
    public async Task<ActionResult> SendHit([FromQuery] string? id)
    {
        var client = new HubConnectionBuilder()
            .WithUrl($"http://{Request.Host}/facereg")
            .Build();
        
        await client.StartAsync();
        await client.SendAsync("BroadcastMessage", "FaceReg", id);
        await client.StopAsync();
        return Json(new { Message = "Done!", UserID = id });
    }

    [HttpGet("facereg_listener")]
    public IActionResult Index()
    {
        ViewData["HubURL"] = $"http://{Request.Host}/facereg";
        return View();
    }

    [HttpPost("facereg_listener/validate")]
    public ActionResult ValidateFace([FromQuery] ulong? id)
    {
        if (id == null)
        {
            return StatusCode(404, new { Message = "Data tidak ditemukan" });
        }

        return StatusCode(400, new { Message = "Not implemented" });
    }
}
