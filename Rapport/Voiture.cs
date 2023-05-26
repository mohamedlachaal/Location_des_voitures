using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport
{
    class Voiture
    {
        public int id { get; set; }
        public string matricule { get; set; }
        public int nbr_portes { get; set; }
        public int nbr_places { get; set; }
        public string photo_1 { get; set; }
        public string couleur { get; set; }
        public Marque Marque { get; set; }
        public int MarqueId { get; set; }
    }
}
