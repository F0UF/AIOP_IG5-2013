using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AIOPServer.Models
{
    
    [Table("RESERVATION")]
    [DataContract]
    public class Booking
    {

        [Key]
        [Column("ID_RESERVATION")]
        [DataMember]
        public int Id_Booking { get; set; }

        [DataMember]
        public virtual Room Room { get; set; }
        [ForeignKey("Room")]
        [Column("ID_SALLE")]
        [DataMember]
        public int Id_Room { get; set; }

        [DataMember]
        public virtual Teaching Teaching { get; set; }
        [ForeignKey("Teaching")]
        [Column("ID_ENSEIGNEMENT")]
        [DataMember]
        public int Id_Teaching { get; set; }

        [DataMember]
        [Column("HEURE_DEBUT")]
        public DateTime Start_Date { get; set; }

        [DataMember]
        [Column("HEURE_FIN")]
        public DateTime End_Date { get; set; }

        [DataMember]
        [Column("ETAT")]
        public String State { get; set; }

        public static IEnumerable<Booking> getWaitingBooking(AIOPContext db)
        {
            IEnumerable<Booking> bookings = null;
            bookings = db.Bookings.Where(b => b.State == "En attente");
            return bookings;
        }

        public static Booking acceptBooking(AIOPContext db,int id_Booking)
        {
            Booking bk = db.Bookings.Find(id_Booking);
            try
            {
                bk.State = "Validé";
                db.SaveChanges();
                return bk;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Booking refuseBooking(AIOPContext db, int id_Booking)
        {
            Booking bk = db.Bookings.Find(id_Booking);
            try
            {
                bk.State = "Refusé";
                db.SaveChanges();
                return bk;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}