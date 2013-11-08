using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Login.Models;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext()
            : base("name=LoginContext")
        {
        }

        
        public DbSet<LoginModel> Logins { get; set; }
    }
}