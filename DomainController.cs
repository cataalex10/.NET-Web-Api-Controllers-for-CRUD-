using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HrBackend.Models;

namespace HrBackend.Controllers
{
    public class DomainController : ApiController
    {
        private HrManagementEntities db = new HrManagementEntities();

        // GET: api/Domain
        public IQueryable<Domain> GetDomains()
        {
            return db.Domains;
        }

        // GET: api/Domain/5
        [ResponseType(typeof(Domain))]
        public IHttpActionResult GetDomain(int id)
        {
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return NotFound();
            }

            return Ok(domain);
        }

        // PUT: api/Domain/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDomain(int id, Domain domain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != domain.Id)
            {
                return BadRequest();
            }

            db.Entry(domain).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DomainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Domain
        [ResponseType(typeof(Domain))]
        public IHttpActionResult PostDomain(Domain domain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Domains.Add(domain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = domain.Id }, domain);
        }

        // DELETE: api/Domain/5
        [ResponseType(typeof(Domain))]
        public IHttpActionResult DeleteDomain(int id)
        {
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return NotFound();
            }

            db.Domains.Remove(domain);
            db.SaveChanges();

            return Ok(domain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DomainExists(int id)
        {
            return db.Domains.Count(e => e.Id == id) > 0;
        }
    }
}