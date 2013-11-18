using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIOPServer.Models
{
    [Table("BATIMENT")]
    [DataContract]
    public class Building
    {
        [Key]
        [Column("ID_BATIMENT")]
        [DataMember]
        public int Id_Building { get; set; }

        [Column("LIBELLE_BATIMENT")]
        [DataMember]
        public string Building_Name { get; set; }

    }
}