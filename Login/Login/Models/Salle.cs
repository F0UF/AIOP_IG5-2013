using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Salle
    {
        //ID ESPECE DE CONNARD
        public int id { get; set; }
        public Batiment batiment { get; set; }
        public int numero_salle { get; set; }
        public Boolean retroprojecteur { get; set; }
        public Boolean ordinateur { get; set; }
        public int capacite { get; set; }
    }
}