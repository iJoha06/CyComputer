using CyComputer.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace CyComputer.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DataAlmacen()
        {
            SerieAlmacen serie = new SerieAlmacen();
            return Json(serie.GetDataDummy());
        }
    }
}
