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
            IEnumerable <Booking> bookings = null;

            bookings = db.Bookings.Where(b => b.Teaching.Id_Teacher == id_Teacher & b.State == "Validé");

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
                return "Suppression impossible";
            }
        }
    }
}
