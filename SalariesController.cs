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
    public class SalariesController : ApiController
    {
        private HrManagementEntities db = new HrManagementEntities();

        // GET: api/Salaries
        public IQueryable<Salary> GetSalaries()
        {
            return db.Salaries;
        }

        // GET: api/Salaries/5
        [ResponseType(typeof(Salary))]
        public IHttpActionResult GetSalary(int id)
        {
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);
        }

        // PUT: api/Salaries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalary(int id, Salary salary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salary.Id)
            {
                return BadRequest();
            }

            db.Entry(salary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
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

        // POST: api/Salaries
        [ResponseType(typeof(Salary))]
        public IHttpActionResult PostSalary(Salary salary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Salaries.Add(salary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salary.Id }, salary);
        }

        // DELETE: api/Salaries/5
        [ResponseType(typeof(Salary))]
        public IHttpActionResult DeleteSalary(int id)
        {
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            db.Salaries.Remove(salary);
            db.SaveChanges();

            return Ok(salary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalaryExists(int id)
        {
            return db.Salaries.Count(e => e.Id == id) > 0;
        }
    }
}