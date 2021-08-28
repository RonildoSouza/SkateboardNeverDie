using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SkateboardNeverDie.Services.SingleSignOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("~/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("~/terms")]
        public IActionResult Terms()
        {
            return View();
        }

        [Route("~/error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
