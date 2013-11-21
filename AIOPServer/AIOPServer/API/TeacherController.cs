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
        public IEnumerable<Teacher> GetTeachers()
        {
            return Teacher.GetTeachers(db);
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
            int totalHoursToDo;
            float hoursDone;
            float hoursPlan;
            float hoursLeftToPlan;

            totalHoursToDo = Teacher.getHoursToDo(db, id_teacher);

            hoursDone = Teacher.getHoursDone(db, id_teacher, currentDate);

            hoursPlan = Teacher.getHoursPlan(db, id_teacher, currentDate);

            hoursLeftToPlan = totalHoursToDo - hoursPlan - hoursDone;

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

            int totalHoursToDo;
            float hoursDone;
            float hoursPlan;
            float hoursLeftToPlan;

            totalHoursToDo = Teacher.getHoursToDo(db, id_teacher);

            hoursDone = Teacher.getHoursDone(db, id_teacher, currentDate);

            hoursPlan = Teacher.getHoursPlan(db, id_teacher, currentDate);

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