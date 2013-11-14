using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIOPServer.Models
{
    [Table("Salle")]
    public class Room
    {
        [Key]
        [Column("ID_SALLE")]
        public int Id_Room { get; set; }

        [Column("ID_BATIMENT")]
        public int Id_Building { get; set; }

        [Column("NUMERO_SALLE")]
        public string Room_Number { get; set; }

        [Column("RETROPROJECTEUR")]
        public bool Projector { get; set; }

        [Column("ORDINATEUR")]
        public bool Computer { get; set; }

        [Column("CAPACITE")]
        public int Capacity { get; set; }

    }
}