using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport
{
    class Assurance
    {
        public int id { get; set; }
        public string agence { get; set; }
        public DateTime date_debut { get; set; }
        public DateTime date_fin { get; set; }
        public int prix { get; set; }
        public Voiture voiture { get; set; }
        public int VoitureId { get; set; }
    }
}
