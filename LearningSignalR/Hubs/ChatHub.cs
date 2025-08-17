﻿using LearningSignalR.Data;
using Microsoft.AspNetCore.SignalR;

namespace LearningSignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = _db.Users.FirstOrDefault(u => u.Email == receiver).Id;
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            }
        }
    }
}
