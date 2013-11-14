using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AIOPServer.Models;
using Newtonsoft.Json.Linq;

namespace AIOPServer.API
{
    public class AccountController : ApiController
    {

        AIOPContext db = new AIOPContext();

        [HttpGet]
        [ActionName("login")]
        public String GetLogin(string username, string mdp) 
        {
            Room room = db.Rooms.Find(1);
            return "MDP : " + room.Room_Number;
        }

        [HttpPost]
        [ActionName("login")]
        public JObject PostLogin(JObject jsonData)
        {
            dynamic json = jsonData;
            string username = json.username;
            string password = json.password;

            Teacher ens = db.Teachers.SingleOrDefault(user => user.Last_Name == username);

            JObject jo = new JObject();

            if (ens != null)
            {
                if (ens.Password.Equals(password))
                {
                    jo.Add("Status", 1);
                    jo.Add("Message", "User " + username + " is now connected.");
                    jo.Add("id_user", ens.Id_Teacher);
                    jo.Add("name", ens.Last_Name);
                }
                else
                {
                    jo.Add("Status", 0);
                    jo.Add("Message", "Wrong password.");
                }
            }
            else
            {
                jo.Add("Status", 0);
                jo.Add("Message", "Wrong username.");
            }
            return jo;
        }

        [HttpGet]
        [ActionName("logout")]
        public String GetLogout()
        {
            return "Julian Gauthier logout";
        }

    }
}
