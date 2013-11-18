using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Matiere
    {
        public int Id { get; set; }
        public Ue ue { get; set; }
        public Enseignant enseignant_responsable { get; set; }
        public String libelle_matiere { get; set; }

    }
}