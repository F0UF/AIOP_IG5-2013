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

        //Get
        [HttpGet]
        [ActionName("Waiting")]
        public IEnumerable<Booking> GetWaitingBookings()
        {
            return Booking.getWaitingBooking(db);
        }

        [HttpGet]
        [ActionName("CreateTeaching")]
        public Teaching CreateTeaching(String Subject_Name, String Teacher_Name, String Group_Name, String Type, String Course_Name, int Hours)
        {
            int id_Teacher;
            int id_Group;

            String[] names = Teacher_Name.Split(' ');
            String first_name = names[0];
            String last_name = names[1];
            if (names.Length > 2)
            {
                last_name = last_name + " " + names[2];
            }

            id_Teacher = db.Teachers.SingleOrDefault(teacher => teacher.First_Name == first_name && teacher.Last_Name == last_name).Id_Teacher;
            id_Group = db.Groups.SingleOrDefault(group => group.Group_Name == Group_Name).Id_Group;

            Subject subject = db.Subjects.SingleOrDefault(this_subject => this_subject.Subject_Name == Subject_Name);
            CourseType courseType = db.CourseTypes.SingleOrDefault(this_type => this_type.Course_Type_Name == Type);
            Course course = db.Courses.SingleOrDefault(this_course => this_course.Subject.Subject_Name == Subject_Name && this_course.Course_Type.Course_Type_Name == Type);

            if (course == null)
            {
                Course.createCourse(db, subject.Id_Subject, courseType.Id_Course_Type, Course_Name);
                course = db.Courses.SingleOrDefault(this_course => this_course.Subject.Subject_Name == Subject_Name && this_course.Course_Type.Course_Type_Name == Type);
            }

            return Teaching.createTeaching(db, id_Teacher, id_Group, Hours, course.Id_Course);
        }

        [HttpGet]
        [ActionName("Accept")]
        public Booking AcceptBookings(int id_Booking)
        {
            return Booking.acceptBooking(db, id_Booking);
        }

        [HttpGet]
        [ActionName("Refuse")]
        public Booking RefuseBookings(int id_Booking)
        {
            return Booking.refuseBooking(db, id_Booking);
        }

        //Post
        [HttpPost]
        [ActionName("CreateTeaching")]
        public Teaching PostCreateTeaching(JObject json)
        {
            //String Subject_Name, String Teacher_Name, String Group_Name, String Type, String Course_Name, int Hours
            dynamic jo = json;
            String Subject_Name = jo.Subject_Name;
            String Teacher_Name = jo.Teacher_Name;
            String Group_Name = jo.Group_Name;
            String Type = jo.Type;
            String Course_Name = jo.Course_Name;
            int Hours = jo.Hours;


            int id_Teacher;
            int id_Group;

            String[] names = Teacher_Name.Split(' ');
            String first_name = names[0];
            String last_name = names[1];
            if (names.Length > 2)
            {
                last_name = last_name + " " + names[2];
            }

            id_Teacher = db.Teachers.SingleOrDefault(teacher => teacher.First_Name == first_name && teacher.Last_Name == last_name).Id_Teacher;
            id_Group = db.Groups.SingleOrDefault(group => group.Group_Name == Group_Name).Id_Group;

            Subject subject = db.Subjects.SingleOrDefault(this_subject => this_subject.Subject_Name == Subject_Name);
            CourseType courseType = db.CourseTypes.SingleOrDefault(this_type => this_type.Course_Type_Name == Type);
            Course course = db.Courses.SingleOrDefault(this_course => this_course.Subject.Subject_Name == Subject_Name && this_course.Course_Type.Course_Type_Name == Type);

            if (course == null)
            {
                Course.createCourse(db, subject.Id_Subject, courseType.Id_Course_Type, Course_Name);
                course = db.Courses.SingleOrDefault(this_course => this_course.Subject.Subject_Name == Subject_Name && this_course.Course_Type.Course_Type_Name == Type);
            }

            return Teaching.createTeaching(db, id_Teacher, id_Group, Hours, course.Id_Course);
        }

        [HttpPost]
        [ActionName("Accept")]
        public JObject PostAcceptBookings(int id_Booking)
        {
            JObject jo = new JObject();
            jo.Add(JsonConvert.SerializeObject(Booking.acceptBooking(db, id_Booking)));
            return jo;
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
