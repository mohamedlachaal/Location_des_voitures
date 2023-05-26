using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class SignController : Controller
    {
        MyContext db;
        public SignController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Utilisateur utilisateur)
        {
            if(utilisateur.Password == utilisateur.RecPassword)
            {

            db.utilisateurs.Add(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Login","Authentification");
            }
            else
            {
                ModelState.AddModelError("", " password and the repassword are not the same");

                return View();
            }
        }
        public IActionResult Update(int id)
        {
            Utilisateur utilisateur = db.utilisateurs.Find(id);
            if(utilisateur == null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }
        [HttpPost]
        public IActionResult Update(Utilisateur utilisateur)
        {
            if(utilisateur.Password == utilisateur.RecPassword)
            {
                Utilisateur utilisateur1 = db.utilisateurs.Find(utilisateur.id);
                utilisateur1.Nom = utilisateur.Nom;
                utilisateur1.Prenom = utilisateur.Prenom;
                utilisateur1.Username = utilisateur.Username;
                utilisateur1.Password = utilisateur.Password;
                db.utilisateurs.Update(utilisateur1);
                db.SaveChanges();
                return RedirectToAction("Index", "Voiture");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Details(int id)
        {
            var utilisateur = db.utilisateurs.FirstOrDefault(u => u.id ==id );
            if(utilisateur==null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }
    }
}
