using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tpvoiture.Models;

namespace tpvoiture.Controllers
{
    public class AuthentificationController : Controller
    {

        MyContext db;
        public AuthentificationController(MyContext db)
        {
            this.db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {

                var user = db.utilisateurs.Where(u => u.Username == utilisateur.Username && u.Password == utilisateur.Password).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetInt32("userId", user.id);
                    return RedirectToAction("Index", "/Voiture");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");

                    return View();
                }
                
            }
            return View();
        }
    }
}
