using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AIOPServer.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Data.Entity.Infrastructure;
using System.Web;


namespace AIOPServer.API
{
    public class PlanningController : ApiController
    {
        AIOPContext db = new AIOPContext();

        // api/planning/display?id_Teacher=XX
        [HttpGet]
        [ActionName("Display")]
        public IEnumerable<Booking> GetPlanning(int id_Teacher)
        {
            IEnumerable<Booking> bookings = null;

            bookings = db.Bookings.Where(b => b.Teaching.Id_Teacher == id_Teacher & b.State == "Validé");

            return bookings;
        }

        // api/planning/display?group_name=IG4
        [HttpGet]
        [ActionName("Display")]
        public IEnumerable<Booking> GetPlanning(string Group_Name)
        {
            IEnumerable<Booking> bookings = null;

            //bookings = from b in db.Bookings
            //           where (b.Teaching.Group.Group_Name == Group_Name)
            //           select b;

            bookings = db.Bookings.Where(b => b.Teaching.Group.Group_Name.StartsWith(Group_Name) & b.State == "Validé");

            return bookings;
        }




        [HttpGet]
        [ActionName("BookingStatus")]
        public IEnumerable<Booking> GetStatus(int id_Teacher)
        {
            IEnumerable<Booking> bookings = null;

            bookings = db.Bookings.Where(b => b.Teaching.Id_Teacher == id_Teacher);

            return bookings;
        }

        [HttpGet]
        [ActionName("Delete")]
        public String DeleteBooking(int id_Booking)
        {
            try
            {
                using (var context = new AIOPContext())
                {
                    var bk = new Booking { Id_Booking = id_Booking };
                    context.Bookings.Attach(bk);
                    context.Bookings.Remove(bk);
                    context.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e + "Suppression impossible";
            }
        }

        [HttpPost]
        [ActionName("CreateBooking")]
        public void PostBooking(JObject json)
        {
            dynamic jo = json;
            string Group_Name = jo.Group_Name;
            string Subject_Name = jo.Subject_Name;
            string Type = jo.Type;
            bool Projector = jo.Projector;
            bool Computer = jo.Computer;
            DateTime Date = jo.Date;
            DateTime Start_At = jo.Start_At;
            DateTime End_At = jo.End_At;

            JObject result = new JObject();

            IEnumerable<Teaching> teachings = null;

            teachings = db.Teachings.Where(t => t.Course.Course_Type.Course_Type_Name == Type && t.Course.Subject.Subject_Name == Subject_Name);

            if (teachings == null)
            {
                result.Add("Status", 0);
            }
        }

        [HttpGet]
        [ActionName("CreateBooking")]
        public String GetBooking(int Id_Teacher, string Group_Name, string Subject_Name, string Type, bool Projector, bool Computer, int Capacity)
        {
            try
            {
                using (var context = new AIOPContext())
                {
                    DateTime End_Time = DateTime.Today;
                    DateTime Start_Time = DateTime.Today;
                    Teaching teaching = db.Teachings.SingleOrDefault(t => t.Course.Course_Type.Course_Type_Name == Type && t.Course.Subject.Subject_Name == Subject_Name && t.Group.Group_Name == Group_Name && t.Id_Teacher == Id_Teacher);

                    if (teaching == null)
                    {
                        return "teaching null";
                    }

                    IEnumerable<Room> bookedRooms =
                    from booking in db.Bookings
                    where booking.End_Date <= End_Time && booking.Start_Date >= Start_Time
                    select booking.Room;

                    IEnumerable<Room> AllRooms = null;
                    AllRooms = db.Rooms;

                    IEnumerable<Room> FreeRooms = null;
                    FreeRooms = AllRooms.Except(bookedRooms);

                    IEnumerable<Room> GoodRooms = null;
                    GoodRooms = FreeRooms.Where(r => r.Projector == Projector && r.Computer == Computer && r.Capacity >= Capacity);

                    IEnumerable<Room> PerfectRooms = GoodRooms.OrderBy(r => r.Capacity);

                    if (PerfectRooms != null)
                    {
                        Booking bk = new Booking
                            {
                                Id_Teaching = teaching.Id_Teaching,
                                Id_Room = PerfectRooms.First().Id_Room,
                                State = "En attente",
                                Start_Date = Start_Time,
                                End_Date = End_Time,
                            };

                        if (bk == null)
                            return "nulll";

                        context.Bookings.Add(bk);
                        context.SaveChanges();
                        return "ok";
                    }
                    else return "Perfect rooms null";
                }
            }
            catch (Exception e)
            {
                return e + "Réservation impossible";
            }
        }
    }
}
