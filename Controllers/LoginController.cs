using CyComputer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyComputer.Controllers
{
    public class LoginController : Controller
    {
        private readonly CyComputerContext _context;

        public LoginController(CyComputerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombre, string contrasena)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre && u.Contrasena == contrasena);

                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nombre o contraseña incorectos");
                }

            }
            return View();

        }

    }
}