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
using Newtonsoft.Json;


namespace AIOPServer.API
{
    public class AdminController : ApiController
    {
        AIOPContext db = new AIOPContext();
        
        [HttpGet]
        [ActionName("Waiting")]
        public IEnumerable<Booking> GetWaitingBookings()
        {
            return Booking.getWaitingBooking(db);
        }

        [HttpGet]
        [ActionName("Create")]
        public Teaching CreateTeaching(int id_Teacher, int id_Group, int hour_Number, int id_Course)
        {
            return Teaching.createTeaching(db,id_Teacher, id_Group, hour_Number, id_Course);
        }

        [HttpPost]
        [ActionName("Create")]
        public JObject PostCreateTeaching(int id_Teacher, int id_Group, int hour_Number, int id_Course)
        {
            JObject jo = new JObject();
            jo.Add(JsonConvert.SerializeObject(Teaching.createTeaching(db, id_Teacher, id_Group, hour_Number, id_Course)));
            return jo;
        }

        [HttpGet]
        [ActionName("Accept")]
        public Booking AcceptBookings(int id_Booking)
        {
            return Booking.acceptBooking(db, id_Booking);
        }

        [HttpPost]
        [ActionName("Accept")]
        public JObject PostAcceptBookings(int id_Booking)
        {
            JObject jo = new JObject();
            jo.Add(JsonConvert.SerializeObject(Booking.acceptBooking(db, id_Booking)));
            return jo;
        }

        [HttpGet]
        [ActionName("Refuse")]
        public Booking RefuseBookings(int id_Booking)
        {
            return Booking.refuseBooking(db, id_Booking);
        }

        [HttpPost]
        [ActionName("Refuse")]
        public JObject PostRefuseBookings(int id_Booking)
        {
            JObject jo = new JObject();
            jo.Add(JsonConvert.SerializeObject(Booking.refuseBooking(db, id_Booking)));
            return jo;
        }
    }
}
