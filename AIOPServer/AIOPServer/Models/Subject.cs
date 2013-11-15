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
    [Table("MATIERE")]
    [DataContract]
    public class Subject
    {

        [Key]
        [Column("ID_MATIERE")]
        [DataMember]
        public int Id_Subject{ get; set; }

        [DataMember]
        public virtual Module Module { get; set; }
        [ForeignKey("Module")]
        [Column("ID_UE")]
        [DataMember]
        public int Id_Module { get; set; }

        [DataMember]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")]
        [DataMember]
        public int Id_Teacher { get; set; }

        [DataMember]
        [Column("LIBELLE_MATIERE")]
        public string Subject_Name { get; set; }


    }
}