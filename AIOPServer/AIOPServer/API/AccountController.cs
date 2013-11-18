using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AIOPServer.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace AIOPServer.API
{
    public class AccountController : ApiController
    {

        AIOPContext db = new AIOPContext();

        //Get
        [HttpGet]
        [ActionName("login")]
        public JObject GetLogin(string username) 
        {
            Teacher ens = Teacher.getTeacher(db, username);
            JObject j = new JObject();
            dynamic jo = j;
            jo.Add("Message", "Username saught : " + username);
            jo.Add(ens);
            dynamic teacher = JsonConvert.SerializeObject(ens) ;
            jo.Teacher = teacher;

            return jo;
        }

        [HttpGet]
        [ActionName("logout")]
        public String GetLogout()
        {
            return "Julian Gauthier logout";
        }


        //Post
        [HttpPost]
        [ActionName("login")]
        public JObject PostLogin(JObject jsonData)
        {
            dynamic json = jsonData;
            string username = json.Username;
            string password = json.Password;

            Teacher ens = Teacher.getTeacher(db, username);

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
    }
}
