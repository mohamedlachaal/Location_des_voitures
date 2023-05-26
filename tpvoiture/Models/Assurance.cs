using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tpvoiture.Models
{
    public class Assurance
    {
        [Key]
        public int id { get; set; }
        public string agence { get; set; }
        public DateTime date_debut  { get; set; }
        public DateTime date_fin  { get; set; }
        public int prix{ get; set; }
        public Voiture voiture { get; set; }
        public int VoitureId { get; set; }


    }
}
