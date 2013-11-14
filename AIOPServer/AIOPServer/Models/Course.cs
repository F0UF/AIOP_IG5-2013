using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("COURS")]
    public class Course
    {
        [Key]
        [Column("ID_COURS")]
        public int Id_Course { get; set; }

        public virtual Subject Subject { get; set; }
        [ForeignKey("Subject")]
        [Column("ID_MATIERE")]
        public int Id_Subject { get; set; }

        public virtual CourseType Course_Type { get; set; }
        [ForeignKey("Course_Type")]
        [Column("ID_TYPE_DE_COURS")]
        public int Id_Course_Type { get; set; }

        [Column("LIBELLE_COURS")]
        public string Course_Name { get; set; }


    }
}