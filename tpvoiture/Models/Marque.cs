using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Marque
    {
        public int id { get; set; }
        public string libelle { get; set; }
        public List<Voiture> voitures { get; set; }


    }
}
