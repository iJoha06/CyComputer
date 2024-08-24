using CyComputer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyComputer.Controllers
{
    public class GuiaEntradaController : Controller
    {
        private readonly CyComputerContext _context;

        public GuiaEntradaController(CyComputerContext context)
        {
            _context = context;
        }

        // GET: GuiaEntrada
        public IActionResult Index()
        {
            var guiasEntrada = _context.GuiaEntrada
                .Include(g => g.IdProveedorNavigation)
                .Include(g => g.IdAlmacenNavigation)
                .Include(g => g.DetalleEntrada)
                .ToList();

            return View(guiasEntrada);
        }

        // GET: GuiaEntrada/Create
        public IActionResult Create()
        {
            ViewData["Proveedores"] = _context.Proveedores.ToList();
            ViewData["Productos"] = _context.Productos.ToList();
            ViewData["Almacens"] = _context.Almacens.ToList();
            return View();
        }

        // POST: GuiaEntrada/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdProveedor, IdAlmacen, Fecha, Descripcion")] GuiaEntradum guiaEntrada, List<DetalleEntradum> detalleEntradas)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Proveedores"] = _context.Proveedores.ToList();
                ViewData["Productos"] = _context.Productos.ToList();
                ViewData["Almacens"] = _context.Almacens.ToList();
                return View(guiaEntrada);
            }

            // Validar los detalles de la entrada
            foreach (var detalle in detalleEntradas)
            {
                if (detalle.IdProducto == null || detalle.Cantidad == null || detalle.Precio == null)
                {
                    ModelState.AddModelError("", "Todos los campos de detalle de productos son obligatorios.");
                    ViewData["Proveedores"] = _context.Proveedores.ToList();
                    ViewData["Productos"] = _context.Productos.ToList();
                    ViewData["Almacens"] = _context.Almacens.ToList();
                    return View(guiaEntrada);
                }
            }

            // Guardar la guía de entrada y los detalles
            _context.GuiaEntrada.Add(guiaEntrada);
            _context.SaveChanges();

            foreach (var detalle in detalleEntradas)
            {
                detalle.IdGuiaEntrada = guiaEntrada.IdGuiaEntrada;
                _context.DetalleEntrada.Add(detalle);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guiaEntrada = await _context.GuiaEntrada
                .Include(g => g.IdProveedorNavigation)
                .Include(g => g.IdAlmacenNavigation)
                .Include(g => g.DetalleEntrada)
                .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdGuiaEntrada == id);

            if (guiaEntrada == null)
            {
                return NotFound();
            }

            return View(guiaEntrada);
        }

    }
}
