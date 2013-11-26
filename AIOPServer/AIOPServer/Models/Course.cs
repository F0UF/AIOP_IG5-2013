using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AIOPServer.Models
{
    [Table("COURS")]
    [DataContract]
    public class Course
    {
        [Key]
        [Column("ID_COURS")]
        [DataMember]
        public int Id_Course { get; set; }

        [DataMember]
        public virtual Subject Subject { get; set; }
        [ForeignKey("Subject")]
        [Column("ID_MATIERE")]
        [DataMember]
        public int Id_Subject { get; set; }

        [DataMember]
        public virtual CourseType Course_Type { get; set; }
        [ForeignKey("Course_Type")]
        [Column("ID_TYPE_DE_COURS")]
        [DataMember]
        public int Id_Course_Type { get; set; }

        [DataMember]
        [Column("LIBELLE_COURS")]
        public string Course_Name { get; set; }

        public static Course createCourse(AIOPContext db, int id_Subject, int id_Course_Type, String Course_Name)
        {
            Course course = new Course
            {
                Id_Subject = id_Subject,
                Id_Course_Type = id_Course_Type,
                Course_Name = Course_Name
            };
            try
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return course;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}