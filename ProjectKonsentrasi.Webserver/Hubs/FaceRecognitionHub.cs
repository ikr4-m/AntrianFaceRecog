using Microsoft.AspNetCore.SignalR;

namespace ProjectKonsentrasi.Hubs;
public class FaceRecognitionHub : Hub
{
    public async Task BroadcastMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{user}:{Context.ConnectionId}", message);
    }
}
