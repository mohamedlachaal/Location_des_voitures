using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport
{
    class Client
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string tel { get; set; }

        public float SommeTotal { get; set; }
     
      
        public List<Location> locations { get; set; }
    }
}
