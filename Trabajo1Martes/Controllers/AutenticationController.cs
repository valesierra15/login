using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Trabajo1.Models;
using Microsoft.CodeAnalysis.Scripting;

namespace Trabajo1.Controllers
{
    public class AutenticationController : Controller
    {
        private readonly Trabajo1Context _context;

        public AutenticationController(Trabajo1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Usuario1))
            {
                var user = _context.Usuarios.SingleOrDefault(u => u.Usuario1 == usuario.Usuario1);

                if (user != null)
                {
                    if (user.estado == true)
                    {
                        ModelState.AddModelError("", "Cuenta bloqueada. Contacte al administrador.");
                        return View();
                    }

                    if (BCrypt.Net.BCrypt.Verify(usuario.Pass, user.Pass))
                    {
                        if (user.estado == false)
                        {
                            user.Intentos = 0; // Reinicia los intentos fallidos en caso de éxito
                            _context.SaveChanges();

                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.Usuario1),
                        };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        user.Intentos++;
                        if (user.Intentos >= 3)
                        {
                            user.estado = true;
                        }

                        _context.SaveChanges();
                    }
                }

                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Usuario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Hasheo de la contraseña antes de guardarla
                usuario.Pass = BCrypt.Net.BCrypt.HashPassword(usuario.Pass);
                usuario.estado = false;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Autentication");
        }
    }
}
