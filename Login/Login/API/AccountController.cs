using Login.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login.API
{
    public class AccountController : ApiController
    {

        private LoginContext db = new LoginContext();

        // GET api/account
        public IEnumerable<LoginModel> GetLogins()
        {
            return db.Logins.AsEnumerable();
        }

        // GET api/account/id
        public LoginModel GetMdp(int id)
        {

            LoginModel login = db.Logins.Find(id);
            if (login == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return login;

        }

        // POST api/account
        public string Post([FromBody]LoginModel model)
        {

            LoginModel loginSQL = db.Logins.SingleOrDefault(user => user.Username == model.Username);


            if (model.Password.Equals(loginSQL.Password))
            {
                return "ok";
            }
            else
            {
                return "erreur";
            }
            
        }

    }
}
