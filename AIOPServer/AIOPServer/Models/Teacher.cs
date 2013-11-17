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
    }
}