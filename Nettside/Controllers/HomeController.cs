using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nettside.Models;
using Nettside.Data;

namespace Nettside.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        // Definerer lister som en in-memory-lagring. 
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Action metode for fremvisning av viewet "index".
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CorrectMap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CorrectMap(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                // Legger ny posisjon til "positions" listen
                positions.Add(model);

                // viser oppsummering view etter data har blitt registrert og lagret i positions listen
                return View("CorrectionOverview", positions);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(positions);
        }

        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }

        // Handle form submission to register a new change
        [HttpPost]
        public IActionResult RegisterAreaChange(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid data.");
                }

                var newGeoChange = new GeoChanges
                {
                    GeoJson = geoJson,
                    Description = description
                };

                // Save to the database
                _context.GeoChanges.Add(newGeoChange);
                _context.SaveChanges();

                // Redirect to the overview of changes
                return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Display the overview of registered changes. 
        [HttpGet]
        public IActionResult AreaChangeOverview()
        {
            var changes_cb = _context.GeoChanges.ToList();
            return View(changes_cb);
        }

    }
}
