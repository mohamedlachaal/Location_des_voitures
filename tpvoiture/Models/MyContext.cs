using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class MyContext : DbContext 
    {
        public DbSet<Marque> marques { get; set; }
        public DbSet<Voiture> voitures { get; set; }
        public DbSet<Client> clients{ get; set; }
        public DbSet<Assurance> assurances{ get; set; }
        public DbSet<Location> locations{ get; set; }
        public DbSet<Utilisateur> utilisateurs { get; set; }
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }
    }
}
