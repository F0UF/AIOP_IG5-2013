using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIOPServer.Models
{
    [Table("BATIMENT")]
    public class Building
    {
        [Key]
        [Column("ID_BATIMENT")]
        public int Id_Building { get; set; }

        [Column("LIBELLE_BATIMENT")]
        public string Building_Name { get; set; }

    }
}