using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AIOPServer.Models
{
    [Table("ENSEIGNEMENT")]
    [DataContract]
    public class Teaching
    {
        [Key]
        [Column("ID_ENSEIGNEMENT")]
        [DataMember]
        public int Id_Teaching { get; set; }

        [DataMember]
        public virtual Course Course { get; set; }
        [ForeignKey("Course")]
        [Column("ID_COURS")]
        [DataMember]
        public int Id_Course { get; set; }

        [DataMember]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")]
        [DataMember]
        public int Id_Teacher { get; set; }

        [DataMember]
        public virtual Group Group { get; set; }
        [ForeignKey("Group")]
        [Column("ID_GROUPE")]
        [DataMember]
        public int Id_Group { get; set; }

        [DataMember]
        [Column("NB_HEURE_PREVUE")]
        public int Hour_Number { get; set; }

        public static Teaching createTeaching(AIOPContext db, int id_Teacher, int id_Group, int hour_Number, int id_Course)
        {
            Teaching teaching = new Teaching
                   {
                       Id_Course = id_Course,
                       Id_Group = id_Group,
                       Id_Teacher = id_Teacher,
                       Hour_Number = hour_Number
                   };
            try
            {
                db.Teachings.Add(teaching);
                db.SaveChanges();
                return teaching;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}