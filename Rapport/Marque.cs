using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport
{
    class Marque
    {
        public int id { get; set; }
        public string libelle { get; set; }
        public List<Voiture> voitures { get; set; }
    }
}
