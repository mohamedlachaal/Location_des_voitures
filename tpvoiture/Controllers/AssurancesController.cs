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
    public class AssurancesController : Controller
    {
        private readonly MyContext _context;
        private static int idvoiture;

        public AssurancesController(MyContext context)
        {
            _context = context;
        }

        // GET: Assurances
        public async Task<IActionResult> Index()
        {
            var myContext = _context.assurances.Include(a => a.voiture);
            return View(await myContext.ToListAsync());
        }

        // GET: Assurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assurance = await _context.assurances
                .Include(a => a.voiture)
                .FirstOrDefaultAsync(m => m.id == id);
            if (assurance == null)
            {
                return NotFound();
            }

            return View(assurance);
        }

        // GET: Assurances/Create
        public IActionResult Create(int id)
        {
           idvoiture = id;
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id");
            return View();
        }

        // POST: Assurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,agence,date_debut,date_fin,prix,VoitureId")] Assurance assurance)
        {
            assurance.id = 3;
            assurance.VoitureId = idvoiture;
            if (ModelState.IsValid)
            {
                _context.Add(assurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", assurance.VoitureId);
            return View(assurance);
        }

        // GET: Assurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assurance = await _context.assurances.FindAsync(id);
            if (assurance == null)
            {
                return NotFound();
            }
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", assurance.VoitureId);
            return View(assurance);
        }

        // POST: Assurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,agence,date_debut,date_fin,prix,VoitureId")] Assurance assurance)
        {
            if (id != assurance.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssuranceExists(assurance.id))
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
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", assurance.VoitureId);
            return View(assurance);
        }

        // GET: Assurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assurance = await _context.assurances
                .Include(a => a.voiture)
                .FirstOrDefaultAsync(m => m.id == id);
            if (assurance == null)
            {
                return NotFound();
            }

            return View(assurance);
        }

        // POST: Assurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assurance = await _context.assurances.FindAsync(id);
            _context.assurances.Remove(assurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssuranceExists(int id)
        {
            return _context.assurances.Any(e => e.id == id);
        }
    }
}
