using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Room room { get; set; }
        //On spécifie l'enseignant ici pour prendre en compte le fait que plusieurs enseignants peuvent collaborer sur une UE
        public Teacher teacher { get; set; }
        public Teaching teaching { get; set; }
    }
}