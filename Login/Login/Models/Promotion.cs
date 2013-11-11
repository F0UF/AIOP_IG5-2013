using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        // name = speciality ex: IG5
        public String name { get; set; }
        public int year { get; set; }
        public Dictionary<int, Student> students { get; set; }
    }
}