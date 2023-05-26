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
    public class LocationController : Controller
    {
        MyContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocationController(MyContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                List<Location> locations = db.locations.Include(l => l.client).ToList();

                return View(locations);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [Route("Location/Add/{VoitureId}")]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewData["clientId"] = new SelectList(db.clients, "id", "NomComplet");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [HttpPost]
        [Route("Location/Add/{VoitureId}")]
        public IActionResult Add(Location location)
        {
            db.locations.Add(location);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }/*
        public IActionResult Delete(int id)
        {
           Location location= db.locations.Find(id);
            db.locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }*/
        
        public IActionResult Imprimer(int id)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "Report4.rdlc");
            LocalReport report = new LocalReport(path);

            Location location = db.locations.Find(id);
            var locations = db.locations.Include(a => a.Voiture).Include(c => c.client).Where(d => d.id == id).Select(a => new {
               
                date_debut = a.date_debut,
               



            }).ToList();
            var locatioons = db.locations.Include(a => a.Voiture).Where(d => d.id == id).Select(a => new {


                Marque = a.Voiture.Marque.libelle,
                matricule = a.Voiture.matricule,
                nbr_portes = a.Voiture.nbr_portes,
                nbr_places = a.Voiture.nbr_places,

            }).ToList();
            var locationss = db.locations.Include(c => c.client).Where(d => d.id == id).Select(a => new {



                nom = a.client.nom,
                prenom = a.client.prenom,
                tel = a.client.tel,
                  

            }).ToList();

            /*
            var locations = db.locations.Include(v => v.Voiture).Include(c => c.client).Select(a => new {
                marque = a.Voiture.Marque.libelle,
                matricule = a.Voiture.matricule,
                nbr_portes = a.Voiture.nbr_portes,
                nbr_places = a.Voiture.nbr_places,
                nom = a.client.nom,
                prenom = a.client.prenom,
                tel = a.client.tel,
                somme = a.client.SommeTotal,
                date_debut = a.date_debut,
                date_fin=a.date_fin,



            }).ToList();*/



            report.AddDataSource("DataSet3", locations);
            report.AddDataSource("DataSet1", locatioons);
            report.AddDataSource("DataSet2", locationss);
     

            ReportResult result = report.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf", "Report4");
        }
     
    }
}
