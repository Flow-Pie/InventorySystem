using Microsoft.AspNetCore.SignalR;

namespace WebUI.Hubs
{
    public class YourSignalRHub : Hub
    {
        // hub methods here
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}