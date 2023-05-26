using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class MarquesController : Controller
    {
        private readonly MyContext _context;

        public MarquesController(MyContext context)
        {
            _context = context;
        }   

        // GET: Marques
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.marques.Include(m=>m.voitures).ToListAsync());
        }

        // GET: Marques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marque = await _context.marques
                .FirstOrDefaultAsync(m => m.id == id);
            if (marque == null)
            {
                return NotFound();
            }

            return View(marque);
        }

        // GET: Marques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,libelle")] Marque marque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marque);
        }

        // GET: Marques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marque = await _context.marques.FindAsync(id);
            if (marque == null)
            {
                return NotFound();
            }
            return View(marque);
        }

        // POST: Marques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,libelle")] Marque marque)
        {
            if (id != marque.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarqueExists(marque.id))
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
            return View(marque);
        }

        // GET: Marques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marque = await _context.marques
                .FirstOrDefaultAsync(m => m.id == id);
            if (marque == null)
            {
                return NotFound();
            }

            return View(marque);
        }

        // POST: Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marque = await _context.marques.FindAsync(id);
            _context.marques.Remove(marque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarqueExists(int id)
        {
            return _context.marques.Any(e => e.id == id);
        }
    }
}
