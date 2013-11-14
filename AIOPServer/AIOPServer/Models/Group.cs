using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIOPServer.Models
{
    [Table("GROUPE")]
    public class Group
    {

        [Key]
        [Column("ID_GROUPE")]
        public int Id_Group { get; set; }

        public virtual Group Father_Group { get; set; }
        [ForeignKey("Father_Group")]
        [Column("ID_GROUPE_A_POUR_PERE")]
        public int Id_Father_Group{ get; set; }

        [Column("LIBELLE_GROUPE")]
        public string Group_Name{ get; set; }

    }
}