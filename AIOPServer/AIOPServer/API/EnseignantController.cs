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
    public class EnseignantController : ApiController
    {
        private AIOPContext db = new AIOPContext();

        // GET api/Enseignant
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

        // PUT api/Enseignant/5
        public HttpResponseMessage PutEnseignant(int id, Teacher enseignant)
        {
            if (ModelState.IsValid && id == enseignant.Id_Teacher)
            {
                db.Entry(enseignant).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Enseignant
        public HttpResponseMessage PostEnseignant(Teacher enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(enseignant);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, enseignant);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = enseignant.Id_Teacher }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Enseignant/5
        public HttpResponseMessage DeleteEnseignant(int id)
        {
            Teacher enseignant = db.Teachers.Find(id);
            if (enseignant == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Teachers.Remove(enseignant);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, enseignant);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}