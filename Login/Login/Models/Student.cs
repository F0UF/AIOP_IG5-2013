using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Student : Person
    {
        public String ine { get; set; }
        public int group { get; set; }
        public String option { get; set; }
    }
}