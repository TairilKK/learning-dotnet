using Microsoft.AspNetCore.SignalR;

namespace LearningSignalR.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return StaticDetails.DealthyHallowRace;
        }
    }
}
