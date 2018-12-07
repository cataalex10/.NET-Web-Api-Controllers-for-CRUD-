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
    public class CandidateController : ApiController
    {
        private HrManagementEntities db = new HrManagementEntities();
        // GET: api/Candidate
        public IQueryable<Candidate> GetCandidate()
        {
            return db.Candidates;
        }

        // GET: api/Candidate/5
        [ResponseType(typeof(Candidate))]
        public IHttpActionResult GetCandidate(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }

        // PUT: api/Candidate/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCandidate(int id, Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candidate.Id)
            {
                return BadRequest();
            }

            db.Entry(candidate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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

        // POST: api/Candidate
        [ResponseType(typeof(Candidate))]
        public IHttpActionResult PostCandidate(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Candidates.Add(candidate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CandidateExists(candidate.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = candidate.Id }, candidate);
        }

        // DELETE: api/Candidate/5
        [ResponseType(typeof(Candidate))]
        public IHttpActionResult DeleteCandidate(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            db.Candidates.Remove(candidate);
            db.SaveChanges();

            return Ok(candidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CandidateExists(int id)
        {
            return db.Candidates.Count(e => e.Id == id) > 0;
        }
    }
}