using CyComputer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CyComputer.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DataCompras()
        {
            SerieCompras serie = new SerieCompras();
            return Json(serie.GetDataDummy());
        }
    }
}
