using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Enseignement
    {
        public int id { get; set; }
        public Cours cours { get; set; }
        public Enseignant enseignant { get; set; }
        public Groupe groupe { get; set; }
        public float nb_heure_prevue { get; set; }
    }
}