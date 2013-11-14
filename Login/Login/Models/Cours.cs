using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public Matiere matiere { get; set; }
        public Type_de_cours type_de_cours { get; set; }
        public String libelle_cours { get; set; }
    }
}