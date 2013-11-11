using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Teaching
    {
        public int Id { get; set; }
        public String name { get; set; }
        public int nb_hours { get; set; }
        //Dans une optique de gestion des priorités
        public Boolean computer_needed { get; set; }
        //On spécifie la promo ici plutôt que dans une réservation car on optimise le stockage
        public Promotion promotion { get; set; } 
    }
}