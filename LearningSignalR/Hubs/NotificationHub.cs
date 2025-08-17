using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace LearningSignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public static int notifficationCount = 0;
        public static List<string> notifications = new List<string>();

        public async Task SendNotification(string message)
        {
            if (!message.IsNullOrEmpty())
            {
                notifications.Add(message);
                notifficationCount++;
                await LoadMessages();
            }
        }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotifications", notifications, notifficationCount);
        }
    }
}
