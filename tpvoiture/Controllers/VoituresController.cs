using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class VoituresController : Controller
    {
        private readonly MyContext _context;

        public VoituresController(MyContext context)
        {
            _context = context;
        }

        // GET: Voitures
        public async Task<IActionResult> Index()
        {
            var myContext = _context.voitures.Include(v => v.Marque).Include(v=>v.assurances).Include(v=>v.locations);
            foreach(Voiture voiture in myContext)
            {
                float SommeAssurance = 0;
                float SommeLocation = 0;
                foreach (Assurance assurance in voiture.assurances)
                {
                    SommeAssurance = SommeAssurance + assurance.prix;
                }
                voiture.SommeAssurance = SommeAssurance;
                
                foreach(Location location in voiture.locations)
                {
                    SommeLocation = SommeLocation + location.prixjour;
                }
                voiture.SommeLocation = SommeLocation;
            }
            
            return View(await myContext.ToListAsync());
        }

        // GET: Voitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.voitures
                .Include(v => v.Marque)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // GET: Voitures/Create
        public IActionResult Create()
        {
            ViewData["MarqueId"] = new SelectList(_context.marques, "id", "id");
            return View();
        }

        // POST: Voitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,matricule,nbr_portes,nbr_places,photo_1,couleur,MarqueId")] Voiture voiture, IFormFile image)
        {
            string[] extentionImage = new string[] { ".jpeg", ".png", ".jpg" };
            foreach (string item in extentionImage)
            {

                if (image.FileName.EndsWith(item)) 
            {
                    string fileName = image.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    image.CopyToAsync(stream);
                    voiture.photo_1 = fileName;
                    _context.Add(voiture);
                     await _context.SaveChangesAsync();
                      return RedirectToAction(nameof(Index));
            }   
            }
            ViewData["MarqueId"] = new SelectList(_context.marques, "id", "id", voiture.MarqueId);
            return View(voiture);
        }

        // GET: Voitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.voitures.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }
            ViewData["MarqueId"] = new SelectList(_context.marques, "id", "id", voiture.MarqueId);
            return View(voiture);
        }

        // POST: Voitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,matricule,nbr_portes,nbr_places,photo_1,couleur,MarqueId")] Voiture voiture, IFormFile image)
        {
            if (id != voiture.id)
            {
                return NotFound();
            }

           

              
                    _context.Update(voiture);
                    string[] extentionImage = new string[] { ".jpeg", ".png", ".jpg" };
                    foreach (string item in extentionImage)
                    {
                        if (image.FileName.EndsWith(item))
                        {
                            string fileName = image.FileName;
                            fileName = Path.GetFileName(fileName);
                            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            var stream = new FileStream(uploadFilePath, FileMode.Create);
                            image.CopyToAsync(stream);
                            voiture.photo_1 = fileName;
                            await _context.SaveChangesAsync();


                            return RedirectToAction(nameof(Index));
                        }
                    }
                   
                   
            
            ViewData["MarqueId"] = new SelectList(_context.marques, "id", "id", voiture.MarqueId);
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.voitures
                .Include(v => v.Marque)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voiture == null)
            {
                return NotFound();
            }
            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           /* int numberLocationOfVoiture = _context.locations.Where(l => l.retour == false && l.VoitureId == id).Count();
            if(numberLocationOfVoiture==0)
            {
                List<Assurance> assur = _context.assurances.Where(a => a.VoitureId==id).ToList();
                if(assur.Count()!=0)
                {
                    foreach(Assurance assurance in assur)
                    {
                        _context.assurances.Remove(assurance);
                    }
                }*/
            var voiture =  _context.voitures.Find(id);
            _context.voitures.Remove(voiture);
            _context.SaveChanges();
            //}
            return RedirectToAction(nameof(Index));
        }

        private bool VoitureExists(int id)
        {
            return _context.voitures.Any(e => e.id == id);
        }
    }
}
