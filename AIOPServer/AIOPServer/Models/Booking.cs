using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("Reservation")]
    public class Booking
    {

        [Key]
        [Column("ID_RESERVATION")]
        public int Id_Booking { get; set; }

        [Column("ID_SALLE")]
        public int Id_Room { get; set; }

        [Column("ID_ENSEIGNANT")]
        public int Id_Teacher { get; set; }

        [Column("HEURE_DEBUT")]
        public DateTime Start_Date { get; set; }

        [Column("HEURE_FIN")]
        public DateTime End_Date { get; set; }
    }
}