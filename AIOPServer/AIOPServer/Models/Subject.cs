﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("MATIERE")]
    public class Subject
    {

        [Key]
        [Column("ID_MATIERE")]
        public int Id_Subject{ get; set; }

        public virtual Module Module { get; set; }
        [ForeignKey("Module")]
        [Column("ID_UE")]
        public int Id_Module { get; set; }

        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")]
        public int Id_Teacher { get; set; }

        [Column("LIBELLE_MATIERE")]
        public string Subject_Name { get; set; }


    }
}