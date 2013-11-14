using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("ENSEIGNEMENT")]
    public class Teaching
    {
        [Key]
        [Column("ID_ENSEIGNEMENT")]
        public int Id_Teaching { get; set; }

        public virtual Course Course { get; set; }
        [ForeignKey("Course")]
        [Column("ID_COURS")]
        public int Id_Course { get; set; }

        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")]
        public int Id_Teacher { get; set; }

        public virtual Group Group { get; set; }
        [ForeignKey("Group")]
        [Column("ID_GROUPE")]
        public int Id_Group { get; set; }

        [Column("NB_HEURE_PREVUE")]
        public int Hour_Number { get; set; }


    }
}