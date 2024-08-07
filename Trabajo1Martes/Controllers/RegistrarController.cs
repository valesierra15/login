using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabajo1.Models;

namespace Trabajo1.Controllers
{
    public class RegistrarController : Controller
    {
        private readonly Trabajo1Context _context;

        public RegistrarController(Trabajo1Context context)
        {
            _context = context;
        }

        // GET: Registrars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Registrar.ToListAsync());
        }

        // GET: Registrars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrar == null)
            {
                return NotFound();
            }

            return View(registrar);
        }

        // GET: Registrars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,Email,Pass")] Registrar registrar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registrar);
        }

        // GET: Registrars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar.FindAsync(id);
            if (registrar == null)
            {
                return NotFound();
            }
            return View(registrar);
        }

        // POST: Registrars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,Email,Pass")] Registrar registrar)
        {
            if (id != registrar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrarExists(registrar.Id))
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
            return View(registrar);
        }

        // GET: Registrars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrar == null)
            {
                return NotFound();
            }

            return View(registrar);
        }

        // POST: Registrars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrar = await _context.Registrar.FindAsync(id);
            if (registrar != null)
            {
                _context.Registrar.Remove(registrar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrarExists(int id)
        {
            return _context.Registrar.Any(e => e.Id == id);
        }
    }
}
