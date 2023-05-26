using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Utilisateur
    {
        public int id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Username{ get; set; }
        public String Password{ get; set; }
        [NotMapped]
        public String RecPassword { get; set; }
    }
}
