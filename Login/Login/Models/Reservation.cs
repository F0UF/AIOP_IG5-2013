using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Salle salle { get; set; }
        public Enseignement enseignement { get; set; }
        public DateTime heure_debut { get; set; }
        public DateTime heure_fin { get; set; }
    }
}