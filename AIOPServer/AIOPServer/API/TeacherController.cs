using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AIOPServer.Models;
using Newtonsoft.Json.Linq;

namespace AIOPServer.API
{
    public class TeacherController : ApiController
    {
        private AIOPContext db = new AIOPContext();

        // GET api/teacher/accountsList
        [HttpGet]
        [ActionName("accountsList")]
        public IEnumerable<Teacher> GetEnseignants()
        {
            return db.Teachers.AsEnumerable();
        }

        // GET api/Enseignant/5
        public Teacher GetEnseignant(int id)
        {
            Teacher enseignant = db.Teachers.Find(id);
            if (enseignant == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return enseignant;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        [ActionName("summaryHours")]
        public JObject GetsummaryHours(int id_teacher)
        {
            DateTime currentDate = new DateTime(2012, 3, 12);
            int totalHoursToDo = 0;
            int hoursDone = 0;
            int hoursPlan = 0;
            int hoursLeftToPlan = 0;

            IQueryable<Teaching> teachingQuery =
            from teaching in db.Teachings
            where teaching.Id_Teacher == id_teacher
            select teaching;

            foreach (Teaching teaching in teachingQuery)
            {
                totalHoursToDo += teaching.Hour_Number;
            }

            IQueryable<Booking> bookingHoursDoneQuery =
            from booking in db.Bookings
            where booking.End_Date < currentDate && booking.Teaching.Id_Teacher == id_teacher && booking.State == "Validé"
            select booking;

            foreach (Booking booking in bookingHoursDoneQuery)
            {
                hoursDone += (int)(booking.End_Date - booking.Start_Date).TotalHours;
                //hoursDone += (int)(booking.End_Date - booking.Start_Date).TotalSecond;
            }
            //hoursDone = hoursDone/3600;

            IQueryable<Booking> bookingHoursPlanQuery =
            from booking in db.Bookings
            where booking.Start_Date > currentDate && booking.Teaching.Id_Teacher == id_teacher && booking.State == "Validé"
            select booking;

            foreach (Booking booking in bookingHoursPlanQuery)
            {
                hoursPlan += (int)(booking.End_Date - booking.Start_Date).TotalHours;
                //hoursPlan += (int)(booking.End_Date - booking.Start_Date).TotalSecond;
            }
            //hoursPlan = hoursDone/3600;

            hoursLeftToPlan = totalHoursToDo - hoursPlan - hoursDone;

           // return "Summary : " + totalHoursToDo + " " + hoursPlan + " " + hoursDone + " " + hoursLeftToPlan;

            JObject jo = new JObject();

            jo.Add("Total Hours To Do", totalHoursToDo);
            jo.Add("Hours Plan", hoursPlan);
            jo.Add("Hours Done", hoursDone);
            jo.Add("Hours Left To Plan", hoursLeftToPlan);

            return jo;
        }

        [HttpPost]
        [ActionName("summaryHours")]
        public JObject PostsummaryHours(JObject jsonData)
        {
            dynamic json = jsonData;
            int id_teacher = json.id_teacher;
            DateTime currentDate = json.currentDate;

            int totalHoursToDo = 0;
            int hoursDone = 0;
            int hoursPlan = 0;
            int hoursLeftToPlan = 0;

            IQueryable<Teaching> teachingQuery =
            from teaching in db.Teachings
            where teaching.Id_Teacher == id_teacher
            select teaching;

            foreach (Teaching teaching in teachingQuery)
            {
                totalHoursToDo += teaching.Hour_Number;
            }

            IQueryable<Booking> bookingHoursDoneQuery =
            from booking in db.Bookings
            where booking.End_Date < currentDate && booking.Teaching.Id_Teacher == id_teacher
            select booking;

            foreach (Booking booking in bookingHoursDoneQuery)
            {
                hoursDone += (int)(booking.End_Date - booking.Start_Date).TotalHours;
                //hoursDone += (int)(booking.End_Date - booking.Start_Date).TotalSecond;
            }
            //hoursDone = hoursDone/3600;

            IQueryable<Booking> bookingHoursPlanQuery =
            from booking in db.Bookings
            where booking.Start_Date > currentDate && booking.Teaching.Id_Teacher == id_teacher
            select booking;

            foreach (Booking booking in bookingHoursPlanQuery)
            {
                hoursPlan += (int)(booking.End_Date - booking.Start_Date).TotalHours;
                //hoursPlac += (int)(booking.End_Date - booking.Start_Date).TotalSecond;
            }
            //hoursPlan = hoursDone/3600;

            hoursLeftToPlan = totalHoursToDo - hoursPlan - hoursDone;

            JObject jo = new JObject();
            
            jo.Add("Total Hours To Do", totalHoursToDo);
            jo.Add("Hours Plan", hoursPlan);
            jo.Add("Hours Done", hoursDone);
            jo.Add("Hours Left To Plan", hoursLeftToPlan);

            return jo;
        }
    }
}