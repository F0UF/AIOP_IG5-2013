﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIOPServer.Models
{
    [Table("SALLE")]
    [DataContract]
    public class Room
    {
    
        [Key]
        [Column("ID_SALLE")]
        [DataMember]
        public int Id_Room { get; set; }

        [DataMember]
        public virtual Building Building { get; set; }
        [ForeignKey("Building")]
        [Column("ID_BATIMENT")]
        [DataMember]
        public int Id_Building { get; set; }

        [DataMember]
        [Column("NUMERO_SALLE")]
        public string Room_Number { get; set; }

        [DataMember]
        [Column("RETROPROJECTEUR")]
        public bool Projector { get; set; }

        [DataMember]
        [Column("ORDINATEUR")]
        public bool Computer { get; set; }

        [DataMember]
        [Column("CAPACITE")]
        public int Capacity { get; set; }

    }
}