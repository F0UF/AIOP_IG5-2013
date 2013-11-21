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

        [HttpGet]
        [ActionName("Display")]
        public IEnumerable<Booking> GetPlanning(int id_Teacher)
        {
            return Booking.validBooking(db, id_Teacher);
        }


        [HttpGet]
        [ActionName("BookingStatus")]
        public IEnumerable<Booking> GetStatus(int id_Teacher)
        {
            return Booking.getStatus(db, id_Teacher);
        }


        [HttpGet]
        [ActionName("Display")]
        public IEnumerable<Booking> GetPlanning(string Group_Name)
        {
            //bookings = from b in db.Bookings
            //           where (b.Teaching.Group.Group_Name == Group_Name)
            //           select b;

            return Booking.getPanningGroups(db,Group_Name);
        }


        [HttpGet]
        [ActionName("Delete")]
        public JObject DeleteBooking(int id_Booking)
        {
            JObject jo = new JObject();
            jo.Add("Status", Booking.deleteBooking(db, id_Booking)); 
            return jo;
        }

        [HttpPost]
        [ActionName("CreateBooking")]
        public Booking PostBooking(JObject json)
        {
            dynamic jo = json;
            int Id_Teacher = jo.Id_Teacher;
            int Capacity = jo.Capacity;
            string Group_Name = jo.Group_Name;
            string Subject_Name = jo.Subject_Name;
            string Type = jo.Type;
            bool Projector = jo.Projector;
            bool Computer = jo.Computer;
            DateTime Date = jo.Date;
            DateTime Start_Time = jo.Start_At;
            DateTime End_Time = jo.End_At;

            Teaching teaching = Teaching.existTeaching(db, Id_Teacher, Group_Name, Subject_Name, Type);

            if (teaching == null)
            {
                return null;
            }

            Room Perfect_Room = Room.getPerfectRoom(db, End_Time, Start_Time, Projector, Computer, Capacity);

            if (Perfect_Room != null)
            {
                return Booking.addBooking(db, teaching, Perfect_Room, Start_Time, End_Time);
            }
            else return null;
        }

        [HttpGet]
        [ActionName("CreateBooking")]
        public Booking GetBooking(int Id_Teacher, string Group_Name, string Subject_Name, string Type, bool Projector, bool Computer, int Capacity, DateTime End_Time, DateTime Start_Time)
        {
            Teaching teaching = Teaching.existTeaching(db, Id_Teacher, Group_Name, Subject_Name, Type);

            if (teaching == null)
            {
                return null;
            }

            Room Perfect_Room = Room.getPerfectRoom(db, End_Time, Start_Time, Projector, Computer, Capacity);

            if (Perfect_Room != null)
            {
                return Booking.addBooking(db, teaching, Perfect_Room, Start_Time, End_Time);
            }
            else return null;
        }
    }
}
