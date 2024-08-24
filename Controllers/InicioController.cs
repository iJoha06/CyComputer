using Microsoft.AspNetCore.Mvc;

namespace CyComputer.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerIndex()
        {
            return View();
        }

    }
}
