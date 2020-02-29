using Microsoft.AspNetCore.Mvc;

namespace Fbits.VueMpaTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SampleApi()
        {
            return View();
        }
    }
}
