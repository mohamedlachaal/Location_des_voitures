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
    public class LocationsController : Controller
    {
        private readonly MyContext _context;

        public LocationsController(MyContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var myContext = _context.locations.Include(l => l.Voiture).Include(l => l.client);
            return View(await myContext.ToListAsync());
        }
        public IActionResult UpdateRetour(int id)
        {
            bool retourVoiture = _context.locations.Where(e => e.VoitureId == id).Select(l => l.retour).FirstOrDefault();
            if (retourVoiture == false)
            {
                retourVoiture = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Voitures");
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations
                .Include(l => l.Voiture)
                .Include(l => l.client)
                .FirstOrDefaultAsync(m => m.id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id");
            ViewData["ClientId"] = new SelectList(_context.clients, "id", "id");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,date_debut,date_fin,retour,prixjour,ClientId,VoitureId")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", location.VoitureId);
            ViewData["ClientId"] = new SelectList(_context.clients, "id", "id", location.ClientId);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", location.VoitureId);
            ViewData["ClientId"] = new SelectList(_context.clients, "id", "id", location.ClientId);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,date_debut,date_fin,retour,prixjour,ClientId,VoitureId")] Location location)
        {
            if (id != location.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.id))
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
            ViewData["VoitureId"] = new SelectList(_context.voitures, "id", "id", location.VoitureId);
            ViewData["ClientId"] = new SelectList(_context.clients, "id", "id", location.ClientId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations
                .Include(l => l.Voiture)
                .Include(l => l.client)
                .FirstOrDefaultAsync(m => m.id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.locations.FindAsync(id);
            _context.locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.locations.Any(e => e.id == id);
        }
    }
}
