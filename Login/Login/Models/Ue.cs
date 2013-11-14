using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Ue
    {
        public int Id { get; set; }
        public Enseignant enseignant { get; set; }
        public String libelle_UE { get; set; }
    }
}