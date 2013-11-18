using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIOPServer.Models
{
    [Table("ENSEIGNANT")]
    [DataContract]
    public class Teacher
    {
        [Key]
        [Column("ID_ENSEIGNANT")]
        [DataMember]
        public int Id_Teacher { get; set; }

        [Column("NOM")]
        [DataMember]
        public string Last_Name { get; set; }

        [Column("PRENOM")]
        [DataMember]
        public string First_Name { get; set; }

        [Column("MDP")]
        public string Password { get; set; }

        [Column("SUPER_USER")]
        [DataMember]
        public int Super_User { get; set; }

        public static Teacher getTeacher(AIOPContext db, string username)
        {
            return db.Teachers.SingleOrDefault(user => user.Last_Name == username);
        }

        public static int getHoursToDo(AIOPContext db, int id_teacher)
        {
            int totalHoursToDo = 0;

            IQueryable<Teaching> teachingQuery =
            from teaching in db.Teachings
            where teaching.Id_Teacher == id_teacher
            select teaching;

            foreach (Teaching teaching in teachingQuery)
            {
                totalHoursToDo += teaching.Hour_Number;
            }

            return totalHoursToDo;
        }

        public static int getHoursDone(AIOPContext db, int id_teacher, DateTime currentDate)
        {
            int hoursDone = 0;

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

            return hoursDone;
        }

        public static int getHoursPlan(AIOPContext db, int id_teacher, DateTime currentDate)
        {
            int hoursPlan = 0;

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

            return hoursPlan;
        }
    }
}