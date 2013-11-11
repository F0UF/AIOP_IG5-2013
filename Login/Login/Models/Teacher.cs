using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Teacher
    {
        public int hours_due { get; set; }
        // Ici ont pourrait calculer hours_done mais on duplique les données pour eviter une requête lourde
        public int hours_done { get; set; }
    }
}