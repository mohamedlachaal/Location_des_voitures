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
    public class ClientController : Controller
    {
        MyContext db;
        public ClientController(MyContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                List<Client> Clients = db.clients.Include(c => c.locations).ToList();
                foreach (Client client in Clients)
                {
                    float prixTotal = 0;
                    float prixTotalTrue = 0;

                    foreach (Location location in client.locations)
                    {
                        if (location.retour == false)
                        {
                            double c = (location.date_fin - location.date_debut).TotalDays;
                            prixTotalTrue = (float)c * location.prixjour;
                        }
                        else
                        {
                            double v = (location.date_fin - location.date_debut).TotalDays;
                            prixTotal = (float)v * location.prixjour;
                        }
                        //  float v = ((float)DbFunctions.DiffDays(location.date_debut, location.date_fin);


                    }
                    client.SommeTotal = prixTotal + prixTotalTrue;
                }

                return View(Clients);
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
        public IActionResult ReturnFalse()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                List<Client> Clients = db.clients.Include(c => c.locations).ToList();
                foreach (Client client in Clients)
                {
                    float prixTotal = 0;
                    float prixTotalTrue = 0;

                    foreach (Location location in client.locations)
                    {
                        if (location.retour == false)
                        {
                            double c = (location.date_fin - location.date_debut).TotalDays;
                            prixTotalTrue = (float)c * location.prixjour;
                        }
                        else
                        {
                            double v = (location.date_fin - location.date_debut).TotalDays;
                            prixTotal = (float)v * location.prixjour;
                        }
                        //float v = ((float)DbFunctions.DiffDays(location.date_debut, location.date_fin);


                    }
                    client.SommeTotal = prixTotal + prixTotalTrue;
                }
                return View(Clients);
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
        public IActionResult Add(Client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
            return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                int numLocation = db.locations.Where(l => l.ClientId == id).Count();
                if (numLocation == 0)
                {
                    Client client = db.clients.Find(id);
                    db.clients.Remove(client);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.error = "y'on a des locations associees avec des client";

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Authentification");
            }
        }
    }
}
