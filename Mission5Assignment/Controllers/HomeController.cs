using Microsoft.AspNetCore.Mvc;
using Mission5Assignment.Services;

namespace Mission5Assignment.Controllers
{
    // This controller handles requests for the Home section of the site
    public class HomeController : Controller
    {
        // This is a private field that will store our API service
        private readonly SmashBrosApiService _api;

        // Constructor for the controller
        // ASP.NET automatically injects the SmashBrosApiService here
        // because we registered it in Program.cs
        public HomeController(SmashBrosApiService api)
        {
            _api = api;
        }

        // This action loads the main home page
        // URL: /Home/Index  or just /
        public IActionResult Index() => View();

        // This action loads the tutor calculator page
        // URL: /Home/Tutor
        public IActionResult Tutor() => View();

        // This action loads the Smash Characters page
        // It calls the external API to get character data
        public async Task<IActionResult> Characters()
        {
            // Call the API service to get the list of characters
            var characters = await _api.GetUltimateCharactersAsync();

            // Pass the character list into the view
            // The view will use this data to display the characters
            return View(characters);
        }
    }
}