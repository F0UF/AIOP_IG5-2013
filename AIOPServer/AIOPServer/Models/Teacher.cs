using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIOPServer.Models
{
    [Table("ENSEIGNANT")]
    public class Teacher
    {
        [Key]
        [Column("ID_ENSEIGNANT")]
        public int Id_Teacher { get; set; }
        [Column("NOM")]
        public string Last_Name { get; set; }
        [Column("PRENOM")]
        public string First_Name { get; set; }
        [Column("MDP")]
        public string Password { get; set; }
        [Column("SUPER_USER")]
        public int Super_User { get; set; }
    }
}