using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class VoitureController : Controller
    {
        MyContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VoitureController(MyContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("username")!=null)
            {

            List<Voiture> voitures = db.voitures.Include(v => v.assurances).Include(m => m.Marque).Include(l=>l.locations).ToList();
            foreach(Voiture voiture in voitures)
            {
            float totalAssurance = 0;
            float totalLocations = 0;
                foreach(Assurance assurance in voiture.assurances)
                {
                    totalAssurance = totalAssurance + assurance.prix;
                }
                voiture.SommeAssurance = totalAssurance;
                foreach(Location location in voiture.locations)
                {
                    totalLocations = totalLocations + location.prixjour;
                }
                voiture.SommeLocation = totalLocations;
            }
            
            return View(voitures);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("username") != null)
            {

                ViewData["MarqueId"] = new SelectList(db.marques, "id", "id");
            return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [HttpPost]
        public IActionResult Add(Voiture v ,IFormFile image)
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
                    v.photo_1 = fileName;

                    db.voitures.Add(v);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["MarqueId"] = new SelectList(db.marques, "id", "id", v.MarqueId);
            return View(v);
        }
        public IActionResult Details(int id )
        {
            if (HttpContext.Session.GetString("username") != null)
            {

                if (id == null)
            {
                return NotFound();
            }
            var voiture = db.voitures.Include(v => v.Marque).FirstOrDefault(m => m.id == id);
            if(voiture==null)
            {
                return NotFound();
            }
            return View(voiture);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                int location = db.locations.Where(l => l.VoitureId == id && l.retour == false).Count();
                if (location == 0)
                {
                    List<Assurance> assurances = db.assurances.Where(a => a.VoitureId == id).ToList();
                    if (assurances.Count() != 0)
                    {
                        foreach (Assurance assurance in assurances)
                        {
                            db.assurances.Remove(assurance);

                        }
                    }
                    Voiture voiture = db.voitures.Find(id);
                    db.voitures.Remove(voiture);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.error = "y'on a des voitures associees avec des locations";

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        public IActionResult Imprimer()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "Report1.rdlc");
            LocalReport report = new LocalReport(path);

             var voitures = db.voitures.Include(v=>v.Marque).Select(a=>new {
              Marque = a.Marque.libelle,
              matricule = a.matricule,
              nbr_portes=a.nbr_portes,
              nbr_places = a.nbr_places,
          
          
          
          }).ToList();
          

            report.AddDataSource("Voiture",voitures);

            ReportResult result = report.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf", "Report1");

        }
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {

                if (id==null)
            {
                return NotFound();
            }
            Voiture voiture = db.voitures.Find(id);
            if(voiture==null)
            {
                return NotFound();
            }
            ViewData["MarqueId"] = new SelectList(db.marques, "id", "id", voiture.MarqueId);
            return View(voiture);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [HttpPost]
        public IActionResult Update(Voiture voiture,IFormFile image)
        {
            
                Voiture voiture1 = db.voitures.Find(voiture.id);
                voiture1.Marque = voiture.Marque;
                voiture1.matricule = voiture.matricule;
                voiture1.nbr_places = voiture.nbr_places;
                voiture1.nbr_portes = voiture.nbr_portes;
                voiture1.couleur = voiture.couleur;
                if (image == null)
                {
                    voiture1.photo_1 = voiture1.photo_1;
                }
                else
                {
                    string[] extentionImage = new string[] { ".jpeg", ".png", ".jpg" };
                    foreach (string item in extentionImage)
                    {
                        if (image.FileName.EndsWith(item))
                        {
                            string fileName = image.FileName;
                            fileName = Path.GetFileName(fileName);
                            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\cars", fileName);
                            var stream = new FileStream(uploadFilePath, FileMode.Create);
                            image.CopyToAsync(stream);
                            voiture1.photo_1 = fileName;
                        }
                    }
                }
               db.voitures.Update(voiture1);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));


           
        }
    }
}
