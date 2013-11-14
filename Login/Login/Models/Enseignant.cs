using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public Boolean super_user { get; set; }
        public String mdp { get; set; }
    }
}