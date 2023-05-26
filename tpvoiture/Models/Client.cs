using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Client
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string tel { get; set; }
        [NotMapped]
        public float SommeTotal { get; set; }
        [NotMapped]
        public string NomComplet
        {
            get { return nom + " " + prenom; }
        }
        public List<Location> locations { get; set; }
    }
}
