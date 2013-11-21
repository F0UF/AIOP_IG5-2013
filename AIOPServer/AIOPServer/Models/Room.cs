using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIOPServer.Models
{
    [Table("SALLE")]
    [DataContract]
    public class Room
    {
    
        [Key]
        [Column("ID_SALLE")]
        [DataMember]
        public int Id_Room { get; set; }

        [DataMember]
        public virtual Building Building { get; set; }
        [ForeignKey("Building")]
        [Column("ID_BATIMENT")]
        [DataMember]
        public int Id_Building { get; set; }

        [DataMember]
        [Column("NUMERO_SALLE")]
        public string Room_Number { get; set; }

        [DataMember]
        [Column("RETROPROJECTEUR")]
        public bool Projector { get; set; }

        [DataMember]
        [Column("ORDINATEUR")]
        public bool Computer { get; set; }

        [DataMember]
        [Column("CAPACITE")]
        public int Capacity { get; set; }


        public static IEnumerable<Room> getbookedRooms(AIOPContext db, DateTime End_Time, DateTime Start_Time)
        {
            IEnumerable<Room> bookedRooms =
                    from booking in db.Bookings
                    where booking.End_Date <= End_Time && booking.Start_Date >= Start_Time
                    select booking.Room;
            return bookedRooms;
        }

        public static IEnumerable<Room> getFreeRooms(AIOPContext db, DateTime End_Time, DateTime Start_Time)
        {
            IEnumerable<Room> bookedRooms = getbookedRooms(db, End_Time, Start_Time);
            IEnumerable<Room> FreeRooms = db.Rooms.Except(bookedRooms);
            return FreeRooms;
        }

        public static Room getPerfectRoom(AIOPContext db, DateTime End_Time, DateTime Start_Time, bool Projector, bool Computer, int Capacity)
        {
            IEnumerable<Room> FreeRooms = getFreeRooms(db, End_Time, Start_Time);
            if (FreeRooms.Count() == 0)
                return null;

            IEnumerable<Room> GoodRooms = null;
            GoodRooms = FreeRooms.Where(r => r.Projector == Projector && r.Computer == Computer && r.Capacity >= Capacity);
            if (GoodRooms.Count() == 0)
                return null;
            IEnumerable<Room> PerfectRooms = GoodRooms.OrderBy(r => r.Capacity);
            if (PerfectRooms.Count() == 0)
                return null;
            Room perfectRoom = PerfectRooms.First();

            return perfectRoom;
        }

    }
}