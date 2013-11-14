using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("RESERVATION")]
    public class Booking
    {

        [Key]
        [Column("ID_RESERVATION")]
        public int Id_Booking { get; set; }

        public virtual Room Room { get; set; }
        [ForeignKey("Room")]
        [Column("ID_SALLE")]
        public int Id_Room { get; set; }

        public virtual Teaching Teaching { get; set; }
        [ForeignKey("Teaching")]
        [Column("ID_ENSEIGNEMENT")]
        public int Id_Teaching { get; set; }

        [Column("HEURE_DEBUT")]
        public DateTime Start_Date { get; set; }

        [Column("HEURE_FIN")]
        public DateTime End_Date { get; set; }

       
     
    }
}