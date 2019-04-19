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
using shanuMVCUserRoles;

namespace shanuMVCUserRoles.Controllers.api
{
    public class RacunsController : ApiController
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();

        // GET: api/Racuns
        public IQueryable<tblRacun> GettblRacuns()
        {
            return db.tblRacuns;
        }

        // GET: api/Racuns/5
        [ResponseType(typeof(tblRacun))]
        public IHttpActionResult GettblRacun(int id)
        {
            tblRacun tblRacun = db.tblRacuns.Find(id);
            if (tblRacun == null)
            {
                return NotFound();
            }

            return Ok(tblRacun);
        }

        // PUT: api/Racuns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblRacun(int id, tblRacun tblRacun)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRacun.RacunId)
            {
                return BadRequest();
            }

            db.Entry(tblRacun).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRacunExists(id))
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

        // POST: api/Racuns
        [ResponseType(typeof(tblRacun))]
        public IHttpActionResult PosttblRacun(tblRacun tblRacun)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblRacuns.Add(tblRacun);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblRacun.RacunId }, tblRacun);
        }

        // DELETE: api/Racuns/5
        [ResponseType(typeof(tblRacun))]
        public IHttpActionResult DeletetblRacun(int id)
        {
            tblRacun tblRacun = db.tblRacuns.Find(id);
            if (tblRacun == null)
            {
                return NotFound();
            }

            db.tblRacuns.Remove(tblRacun);
            db.SaveChanges();

            return Ok(tblRacun);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblRacunExists(int id)
        {
            return db.tblRacuns.Count(e => e.RacunId == id) > 0;
        }
    }
}