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

    public async Task BroadcastCheckFace(ulong id, string name)
    {
        string json = JsonSerializer.Serialize(new
        {
            ConnectionID = Context.ConnectionId,
            ID = id,
            Name = name
        });
        await Clients.All.SendAsync("ReceiveMessage", json);
    }
}
