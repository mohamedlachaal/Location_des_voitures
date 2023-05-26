using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class MarqueController : Controller
    {
        MyContext db;
        public MarqueController(MyContext db)
        {
            this.db = db;
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                List<Marque> marques = db.marques.Include(m => m.voitures).ToList();
                return View(marques);
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
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        [HttpPost]
        public IActionResult Add(Marque m)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                if (ModelState.IsValid)
                {
                    db.marques.Add(m);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
         //   return View();
        }
      
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                int numberOfCars = db.voitures.Where(m => m.MarqueId == id).Count();
                if (numberOfCars == 0)
                {
                    Marque marque = db.marques.Find(id);
                    db.marques.Remove(marque);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.erreur = "la marque est deja associee avec des voitures";
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
;        }
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                Marque findMarque = db.marques.Find(id);
                if (findMarque == null)
                {
                    return NotFound();
                }
                return View(findMarque);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            
        }
        }
        [HttpPost]
        public IActionResult Update(Marque m)
        {
            if(ModelState.IsValid)
            {
                Marque marque = db.marques.Find(m.id);
                marque.libelle = m.libelle;
                db.marques.Update(marque);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
