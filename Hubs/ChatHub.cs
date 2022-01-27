using Microsoft.AspNetCore.SignalR;

namespace HelloMUDWorld.Server.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task UserConnectedNotification (string user)
    {
        await Clients.All.SendAsync("ReceiveMessage", "[SYSTEM]", $"User '{user}' is now connected.");
    }

    public async Task UserDisconnectedNotification (string user)
    {
        await Clients.All.SendAsync("ReceiveMessage", "[SYSTEM]", $"User '{user}' is now disconnected.");
    }
}