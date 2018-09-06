using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestASPNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> m_Logger;

        public HomeController(ILogger<HomeController> logger)
        {
            m_Logger = logger;
        }

        public IActionResult Index()
        {
            var log = m_Logger as ILogger;
            log?.LogInformation("Nlogger implements Ilogger interface");

            m_Logger.LogInformation("Index page says hello!");
            return View();
        }
    }
}