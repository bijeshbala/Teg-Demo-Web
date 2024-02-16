using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using Teg_DemoWeb.Models;

namespace Teg_DemoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Index file with all venues
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetFromJsonAsync<List<Venue>>("https://teg-demo-webapi.azurewebsites.net/api/Event/venues");

            return View(response);
        }
        
        /// <summary>
        /// Get Events 
        /// </summary>
        /// <param name="selectedVenueId">Select Venue Id</param>
        /// <returns>List of all events</returns>
        [HttpGet]
        public async Task<IActionResult> GetEvents(int selectedVenueId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetFromJsonAsync<List<Event>>($"https://teg-demo-webapi.azurewebsites.net/api/Event/venues/{selectedVenueId}");

            return PartialView("_EventsPartial", response);
        }


        public IActionResult Privacy()
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
