using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ProjectKonsentrasi.Webserver.Controllers;
public class HitSignalRController : Controller
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
}
