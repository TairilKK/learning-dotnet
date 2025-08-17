using LearningSignalR.Hubs;
using LearningSignalR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace LearningSignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _hubContext; // <DeathlyHallowsHub>

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows([FromQuery]string type)
        {
            if (StaticDetails.DealthyHallowRace.ContainsKey(type))
            {
                StaticDetails.DealthyHallowRace[type]++;
            }
            await _hubContext.Clients.All.SendAsync("updateDeathlyHallowCount",
                StaticDetails.DealthyHallowRace[StaticDetails.Cloak],
                StaticDetails.DealthyHallowRace[StaticDetails.Stone],
                StaticDetails.DealthyHallowRace[StaticDetails.Wand]
                );
            return Accepted();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult DeathlyHallowsRace()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
