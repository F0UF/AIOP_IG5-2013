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
    public class SubjectController : ApiController
    {
        private AIOPContext db = new AIOPContext();

        // GET api/Subject
        public IEnumerable<Subject> GetSubjects()
        {
            var subjects = db.Subjects.Include(s => s.Module).Include(s => s.Teacher);
            return subjects.AsEnumerable();
        }

        // GET api/Subject/5
        public Subject GetSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return subject;
        }

        // PUT api/Subject/5
        public HttpResponseMessage PutSubject(int id, Subject subject)
        {
            if (ModelState.IsValid && id == subject.Id_Subject)
            {
                db.Entry(subject).State = EntityState.Modified;

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

        // POST api/Subject
        public HttpResponseMessage PostSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, subject);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = subject.Id_Subject }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Subject/5
        public HttpResponseMessage DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Subjects.Remove(subject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, subject);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}