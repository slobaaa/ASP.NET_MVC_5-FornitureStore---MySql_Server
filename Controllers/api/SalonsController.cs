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
    public class SalonsController : ApiController
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();

        // GET: api/Salons
        public IQueryable<tblSalon> GettblSalons()
        {
            return db.tblSalons;
        }

        // GET: api/Salons/5
        [ResponseType(typeof(tblSalon))]
        public IHttpActionResult GettblSalon(int id)
        {
            tblSalon tblSalon = db.tblSalons.Find(id);
            if (tblSalon == null)
            {
                return NotFound();
            }

            return Ok(tblSalon);
        }

        // PUT: api/Salons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblSalon(int id, tblSalon tblSalon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblSalon.SalonId)
            {
                return BadRequest();
            }

            db.Entry(tblSalon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblSalonExists(id))
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

        // POST: api/Salons
        [ResponseType(typeof(tblSalon))]
        public IHttpActionResult PosttblSalon(tblSalon tblSalon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblSalons.Add(tblSalon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblSalon.SalonId }, tblSalon);
        }

        // DELETE: api/Salons/5
        [ResponseType(typeof(tblSalon))]
        public IHttpActionResult DeletetblSalon(int id)
        {
            tblSalon tblSalon = db.tblSalons.Find(id);
            if (tblSalon == null)
            {
                return NotFound();
            }

            db.tblSalons.Remove(tblSalon);
            db.SaveChanges();

            return Ok(tblSalon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblSalonExists(int id)
        {
            return db.tblSalons.Count(e => e.SalonId == id) > 0;
        }
    }
}