using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Person
    {
        public int Id { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public String email { get; set; }
        public String password { get; set; }
    }
}