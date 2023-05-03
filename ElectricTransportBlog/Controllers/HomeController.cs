using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.Home;
using ElectricTransportBlog.Models;
using System.Diagnostics;

namespace ElectricTransportBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPublicationService publication;
        private readonly IMemoryCache cache;

        public HomeController(ILogger<HomeController> logger, IPublicationService publication, IMemoryCache cache)
        {
            _logger = logger;
            this.publication = publication;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            ViewData[MessageConstants.SuccessMessage] = "Добре дошли!";

            return View(new HomePageModel
            {
                 Publications = this.publication.GetTopThreePublications()
            });
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