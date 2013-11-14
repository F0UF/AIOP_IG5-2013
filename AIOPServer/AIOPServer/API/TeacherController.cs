using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AIOPServer.Models;

namespace AIOPServer.API
{
    public class TeacherController : ApiController
    {
        private AIOPContext db = new AIOPContext();

        // GET api/teacher/accountsList
        [HttpGet]
        [ActionName("accountsList")]
        public IEnumerable<Teacher> GetEnseignants()
        {
            return db.Teachers.AsEnumerable();
        }

        // GET api/Enseignant/5
        public Teacher GetEnseignant(int id)
        {
            Teacher enseignant = db.Teachers.Find(id);
            if (enseignant == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return enseignant;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}