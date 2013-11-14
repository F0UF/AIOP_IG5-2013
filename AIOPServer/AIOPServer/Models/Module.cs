using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("UE")]
    public class Module
    {
        [Key]
        [Column("ID_UE")]
        public int Id_UE { get; set; }

        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        [Column("ID_ENSEIGNANT")] 
        public int Id_Teacher { get; set; }

        [Column("LIBELLE_UE")]
        public string Module_Name { get; set; }

    }
}