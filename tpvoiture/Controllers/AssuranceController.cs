using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tpvoiture.Models;


namespace tpvoiture.Controllers
{
    public class AssuranceController : Controller
    {
        private static int idvoiture;
        MyContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AssuranceController(MyContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                List<Assurance> assurance = db.assurances.Include(a => a.voiture).ToList();
                return View(assurance);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [Route("Assurance/Add/{VoitureId}")]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }

        }
        [HttpPost]
            [Route("Assurance/Add/{VoitureId}")]
        public IActionResult Add(Assurance assurance)
        {


            //assurance.VoitureId = idV;

            //assurance.VoitureId = 5;
    
            db.assurances.Add(assurance);
                db.SaveChanges();
   
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Imprimer()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "Report2.rdlc");
            LocalReport report = new LocalReport(path);

            var assurances = db.assurances.Include(v => v.voiture).Select(a => new {
               
                agence = a.agence,
                date_debut = a.date_debut,
                date_fin = a.date_fin,
                prix = a.prix,
                voiture = a.voiture.matricule,



            }).ToList();


            report.AddDataSource("DataSet", assurances);

            ReportResult result = report.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf", "Report2");
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                Assurance assurance = db.assurances.Find(id);
                db.assurances.Remove(assurance);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }

        }



    }
}
