using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Room
    {
        public int Id { get; set; }
        public String name { get; set; }
        public Boolean projector { get; set; }
        //Nombre d'ordinateurs disponibles (pour ne pas avoir une demande rejettée en cas de travail en binôme)
        public int computer { get; set; }
        public int capacity { get; set; }
        //Duplication pour alléger les requêtes
        public Boolean available { get; set; }
    }
}