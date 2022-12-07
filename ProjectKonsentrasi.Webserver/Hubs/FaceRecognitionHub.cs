using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace ProjectKonsentrasi.Hubs;
public class FaceRecognitionHub : Hub
{
    public async Task BroadcastMessage(string user, string message)
    {
        string json = JsonSerializer.Serialize(new
        {
            User = user,
            ConnectionID = Context.ConnectionId,
            Message = message
        });
        await Clients.All.SendAsync("ReceiveMessage", json);
    }
}
