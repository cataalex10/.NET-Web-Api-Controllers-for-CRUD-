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
    public class SubdepartmentController : ApiController
    {
        private HrManagementEntities db = new HrManagementEntities();

        // GET: api/Subdepartment
        public IQueryable<Subdepartment> GetSubdepartment()
        {
            return db.Subdepartments;
        }

        // GET: api/Subdepartment/5
        [ResponseType(typeof(Subdepartment))]
        public IHttpActionResult GetSubdepartment(int id)
        {
            Subdepartment subdepartment = db.Subdepartments.Find(id);
            if (subdepartment == null)
            {
                return NotFound();
            }

            return Ok(subdepartment);
        }

        // PUT: api/Subdepartment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubdepartment(int id, Subdepartment subdepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subdepartment.Id)
            {
                return BadRequest();
            }

            db.Entry(subdepartment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubdepartmentExists(id))
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

        // POST: api/Subdepartment
        [ResponseType(typeof(Subdepartment))]
        public IHttpActionResult PostSubdepartment(Subdepartment subdepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subdepartments.Add(subdepartment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SubdepartmentExists(subdepartment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = subdepartment.Id }, subdepartment);
        }

        // DELETE: api/Subdepartment/5
        [ResponseType(typeof(Subdepartment))]
        public IHttpActionResult DeleteSubdepartment(int id)
        {
            Subdepartment subdepartment = db.Subdepartments.Find(id);
            if (subdepartment == null)
            {
                return NotFound();
            }

            db.Subdepartments.Remove(subdepartment);
            db.SaveChanges();

            return Ok(subdepartment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubdepartmentExists(int id)
        {
            return db.Subdepartments.Count(e => e.Id == id) > 0;
        }
    }
}