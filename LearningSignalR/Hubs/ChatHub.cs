using Microsoft.AspNetCore.SignalR;

namespace LearningSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }
    }
}
