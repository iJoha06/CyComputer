using CyComputer.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace CyComputer.Controllers
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DataVentas()
        {
            SerieVentas serie = new SerieVentas();
            return Json(serie.GetDataDummy());
        }
    }
}
