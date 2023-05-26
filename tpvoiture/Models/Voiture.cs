using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Voiture
    {
        public int id { get; set; }
        public string matricule { get; set; }
        public int nbr_portes { get; set; }
        public int nbr_places { get; set; }
        public string photo_1 { get; set; }
        public string couleur { get; set; }
        public Marque Marque { get; set; }
        public int MarqueId { get; set; }
        [NotMapped]
        public float SommeAssurance { get; set; }
        [NotMapped]
        public float SommeLocation { get; set; }
        public List<Assurance> assurances { get; set; }
        public List<Location>locations { get; set; }


    }
}
