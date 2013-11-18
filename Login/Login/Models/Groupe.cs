using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Groupe
    {
        public int id { get; set; }
        public Groupe groupe_pere { get; set; }
        public String libelle_groupe { get; set; }
    }
}