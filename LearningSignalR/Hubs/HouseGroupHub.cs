using Microsoft.AspNetCore.SignalR;

namespace LearningSignalR.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupsJoined { get; set; } = new List<string>();
        public async Task JoinGroup(string groupName)
        {
            if (!GroupsJoined.Contains(Context.ConnectionId + ":" + groupName))
            {
                GroupsJoined.Add(Context.ConnectionId + ":" + groupName);

                string groupList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        groupList += str.Split(':')[1] + " ";
                    }
                }

                await Clients.Caller.SendAsync("subscitpionStatus", groupList, groupName.ToLower(), true );
                await Clients.Others.SendAsync("memberAddedToHouse", groupName);

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }
        }
        public async Task LeaveGroup(string groupName)
        {
            if (GroupsJoined.Contains(Context.ConnectionId + ":" + groupName))
            {
                GroupsJoined.Remove(Context.ConnectionId + ":" + groupName);

                string groupList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        groupList += str.Split(':')[1] + " ";
                    }
                }

                await Clients.Caller.SendAsync("subscitpionStatus", groupList, groupName.ToLower(), false );
                await Clients.Others.SendAsync("memberRemovedFromHouse", groupName);

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            }
        }

        public async Task TriggerGroupNotify(string groupName)
        {
            await Clients.Group(groupName).SendAsync("tiggerHouseNotification", groupName);
        }
    }
}
