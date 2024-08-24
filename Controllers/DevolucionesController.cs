using Microsoft.AspNetCore.Mvc;
using CyComputer.Models; // Ajusta esto según tu namespace

namespace TuProyecto.Controllers
{
    public class DevolucionesController : Controller
    {
        private static List<Devolucion> _devoluciones = new List<Devolucion>();

        public IActionResult Index()
        {
            return View(_devoluciones);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                devolucion.Id = _devoluciones.Count > 0 ? _devoluciones.Max(d => d.Id) + 1 : 1;
                _devoluciones.Add(devolucion);
                return RedirectToAction("Index");
            }
            return View(devolucion);
        }

        public IActionResult Edit(int id)
        {
            var devolucion = _devoluciones.FirstOrDefault(d => d.Id == id);
            if (devolucion == null)
            {
                return NotFound();
            }
            return View(devolucion);
        }

        [HttpPost]
        public IActionResult Edit(Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                var existingDevolucion = _devoluciones.FirstOrDefault(d => d.Id == devolucion.Id);
                if (existingDevolucion != null)
                {
                    _devoluciones.Remove(existingDevolucion);
                    _devoluciones.Add(devolucion);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(devolucion);
        }

        public IActionResult Delete(int id)
        {
            var devolucion = _devoluciones.FirstOrDefault(d => d.Id == id);
            if (devolucion == null)
            {
                return NotFound();
            }
            return View(devolucion);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var devolucion = _devoluciones.FirstOrDefault(d => d.Id == id);
            if (devolucion != null)
            {
                _devoluciones.Remove(devolucion);
            }
            return RedirectToAction("Index");
        }

    }
}
