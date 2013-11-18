using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AIOPServer.Models
{
    [Table("UE")]
    [DataContract]
    public class Module
    {
        [Key]
        [Column("ID_UE")]
        [DataMember]
        public int Id_UE { get; set; }

        [DataMember]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")]
        [DataMember]
        public int Id_Teacher { get; set; }

        [DataMember]
        [Column("LIBELLE_UE")]
        public string Module_Name { get; set; }

    }
}