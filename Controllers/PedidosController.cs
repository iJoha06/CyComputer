using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CyComputer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CyComputer.Controllers
{
    public class PedidosController : Controller
    {
        private readonly CyComputerContext _context;

        public PedidosController(CyComputerContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.DetalleCompras
                                        .Include(d => d.IdProductoNavigation)
                                        .Include(d => d.IdCompraNavigation)
                                        .ToListAsync();
            return View(pedidos);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleCompra,IdProducto,Cantidad,CostoUnitario")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                var producto = await _context.Productos.FindAsync(detalleCompra.IdProducto);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                // Actualizar stock del producto
                producto.Stock += detalleCompra.Cantidad;
                _context.Update(producto);

                // Guardar registro del pedido
                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleCompra,IdProducto,Cantidad,CostoUnitario")] DetalleCompra detalleCompra)
        {
            if (id != detalleCompra.IdDetalleCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar stock del producto si se cambia la cantidad
                    var producto = await _context.Productos.FindAsync(detalleCompra.IdProducto);
                    if (producto != null)
                    {
                        // Obtener la cantidad original para ajustar el stock correctamente
                        var cantidadOriginal = _context.DetalleCompras.AsNoTracking().FirstOrDefault(d => d.IdDetalleCompra == id)?.Cantidad ?? 0;
                        producto.Stock -= cantidadOriginal;
                        producto.Stock += detalleCompra.Cantidad;
                        _context.Update(producto);
                    }

                    _context.Update(detalleCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCompraExists(detalleCompra.IdDetalleCompra))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra != null)
            {
                // Ajustar el stock del producto antes de eliminar
                var producto = await _context.Productos.FindAsync(detalleCompra.IdProducto);
                if (producto != null)
                {
                    producto.Stock -= detalleCompra.Cantidad;
                    _context.Update(producto);
                }

                _context.DetalleCompras.Remove(detalleCompra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(int id)
        {
            return _context.DetalleCompras.Any(e => e.IdDetalleCompra == id);
        }
    }
}
