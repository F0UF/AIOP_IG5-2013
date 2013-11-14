using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIOPServer.Models
{
    [Table("TYPECOURS")]
    public class CourseType
    {
        [Key]
        [Column("ID_TYPE_DE_COURS")]
        public int Id_Course_Type { get; set; }

        [Column("LIBELLE_TYPE_DE_COURS")]
        public string Course_Type_Name { get; set; }
    }
}