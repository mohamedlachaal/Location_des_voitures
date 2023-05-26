using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Location
    {
        public int id { get; set; }
        public DateTime date_debut { get; set; }
        public DateTime date_fin { get; set; }
        public bool retour { get; set; }
        public int prixjour { get; set; }
        public Client client { get; set; }
        public int ClientId { get; set; }
        public Voiture Voiture { get; set; }
        public int VoitureId { get; set; }
    }
}
