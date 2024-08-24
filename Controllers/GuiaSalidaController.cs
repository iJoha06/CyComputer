using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CyComputer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CyComputer.Controllers
{
    public class GuiaSalidaController : Controller
    {
        private readonly CyComputerContext _context;

        public GuiaSalidaController(CyComputerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var guiasSalida = _context.GuiaSalida
                .Include(g => g.IdClienteNavigation)
                .Include(g => g.DetalleSalida)
                .ToList();

            return View(guiasSalida);
        }

        public IActionResult Create()
        {
            ViewData["Clientes"] = _context.Clientes.ToList();
            ViewData["Productos"] = _context.Productos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuiaSalidum guiaSalidum, List<DetalleSalidum> detalles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Clientes"] = _context.Clientes.ToList();
                ViewData["Productos"] = _context.Productos.ToList();
                return View(guiaSalidum);
            }

            guiaSalidum.Fecha = DateTime.Now;
            _context.Add(guiaSalidum);
            await _context.SaveChangesAsync();

            foreach (var detalle in detalles)
            {
                detalle.IdGuiaSalida = guiaSalidum.IdGuiaSalida;
                _context.DetalleSalida.Add(detalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guiaSalida = await _context.GuiaSalida
                .Include(g => g.IdClienteNavigation)
                .Include(g => g.DetalleSalida)
                    .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdGuiaSalida == id);

            if (guiaSalida == null)
            {
                return NotFound();
            }

            return View(guiaSalida);
        }

        // GET: GuiasSalida/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guiaSalida = _context.GuiaSalida
                .Include(g => g.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdGuiaSalida == id);

            if (guiaSalida == null)
            {
                return NotFound();
            }

            return View(guiaSalida);
        }

        // POST: GuiasSalida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guiaSalida = await _context.GuiaSalida
                .Include(g => g.DetalleSalida)
                .FirstOrDefaultAsync(m => m.IdGuiaSalida == id);

            if (guiaSalida != null)
            {
                // Eliminar los detalles asociados primero
                _context.DetalleSalida.RemoveRange(guiaSalida.DetalleSalida);

                // Luego eliminar la guía de salida
                _context.GuiaSalida.Remove(guiaSalida);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
