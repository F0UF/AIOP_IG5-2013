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

        public static IEnumerable<Booking> validBooking(AIOPContext db, int id_Teacher)
        {
            return db.Bookings.Where(b => b.Teaching.Id_Teacher == id_Teacher);
        }

        public static IEnumerable<Booking> getStatus(AIOPContext db, int id_Teacher)
        {
            return db.Bookings.Where(b => b.Teaching.Id_Teacher == id_Teacher);
        }

        public static IEnumerable<Booking> getPanningGroups(AIOPContext db, string Group_Name)
        {
            return db.Bookings.Where(b => b.Teaching.Group.Group_Name.StartsWith(Group_Name));
        }
        public static Booking addBooking(AIOPContext db, Teaching teaching ,Room Perfect_Room, DateTime Start_Time, DateTime End_Time)
        {
            Booking bk = new Booking
            {
                Id_Teaching = teaching.Id_Teaching,
                Id_Room = Perfect_Room.Id_Room,
                State = "En attente",
                Start_Date = Start_Time,
                End_Date = End_Time,
            };

            db.Bookings.Add(bk);
            db.SaveChanges();
            return bk;
        }
        public static string deleteBooking(AIOPContext db, int id_booking)
        {
            try
            {

                Booking bk = new Booking { Id_Booking = id_booking };
                db.Bookings.Attach(bk);
                db.Bookings.Remove(bk);
                db.SaveChanges();
                return "ok";
            }
            catch(Exception e)
            {
                return "erreur : " + e;
            }
            /*
            try
            {
                using (var context = new AIOPContext())
                {
                    var bk = new Booking { Id_Booking = id_Booking };
                    context.Bookings.Attach(bk);
                    context.Bookings.Remove(bk);
                    context.SaveChanges();
                    return bk;
                }
            }
            catch (Exception e)
            {
                return null;
            }*/
        }
    }
}