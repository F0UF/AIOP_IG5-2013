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
    public class AdminController : ApiController
    {
        AIOPContext db = new AIOPContext();
        
        [HttpGet]
        [ActionName("Waiting")]
        public IEnumerable<Booking> GetWaitingBookings()
        {
            IEnumerable<Booking> bookings = null;
            bookings = db.Bookings.Where(b => b.State == "En attente");
            return bookings;
        }

        [HttpGet]
        [ActionName("Create")]
        public String CreateTeaching(int id_Teacher, int id_Group, int hour_Number, int id_Course)
        {
            try
            {
               using (var context = new AIOPContext())
                {
                   Teaching teaching = new Teaching
                   {
                       Id_Course = id_Course,
                       Id_Group = id_Group,
                       Id_Teacher = id_Teacher,
                       Hour_Number = hour_Number
                   };

                   context.Teachings.Add(teaching);
                   context.SaveChanges();

                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e+ "Création impossible";
            }
        }

        [HttpGet]
        [ActionName("Accept")]
        public String AcceptBookings(int id_Booking)
        {
            try
            {
                using (var context = new AIOPContext())
                {
                    Booking bk = context.Bookings.Find(id_Booking);
                    bk.State = "Validé";
                    context.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e + "Validation impossible";
            }
        }

        [HttpGet]
        [ActionName("Refuse")]
        public String RefuseBookings(int id_Booking)
        {
            try
            {
                using (var context = new AIOPContext())
                {
                    Booking bk = context.Bookings.Find(id_Booking);
                    bk.State = "Refusé";
                    context.SaveChanges();
                    return "ok";
                }
            }
            catch (Exception e)
            {
                return e + "Refus impossible";
            }
        }


    }
}
