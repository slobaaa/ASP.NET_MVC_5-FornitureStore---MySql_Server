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
    public class KomadNamestajasController : ApiController
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();

        // GET: api/KomadNamestajas
        public IQueryable<tblKomadNamestaja> GettblKomadNamestajas()
        {
            return db.tblKomadNamestajas;
        }

        // GET: api/KomadNamestajas/5
        [ResponseType(typeof(tblKomadNamestaja))]
        public IHttpActionResult GettblKomadNamestaja(int id)
        {
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            if (tblKomadNamestaja == null)
            {
                return NotFound();
            }

            return Ok(tblKomadNamestaja);
        }

        // PUT: api/KomadNamestajas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblKomadNamestaja(int id, tblKomadNamestaja tblKomadNamestaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblKomadNamestaja.KomadNamestajaId)
            {
                return BadRequest();
            }

            db.Entry(tblKomadNamestaja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblKomadNamestajaExists(id))
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

        // POST: api/KomadNamestajas
        [ResponseType(typeof(tblKomadNamestaja))]
        public IHttpActionResult PosttblKomadNamestaja(tblKomadNamestaja tblKomadNamestaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblKomadNamestajas.Add(tblKomadNamestaja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblKomadNamestaja.KomadNamestajaId }, tblKomadNamestaja);
        }

        // DELETE: api/KomadNamestajas/5
        [ResponseType(typeof(tblKomadNamestaja))]
        public IHttpActionResult DeletetblKomadNamestaja(int id)
        {
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            if (tblKomadNamestaja == null)
            {
                return NotFound();
            }

            db.tblKomadNamestajas.Remove(tblKomadNamestaja);
            db.SaveChanges();

            return Ok(tblKomadNamestaja);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblKomadNamestajaExists(int id)
        {
            return db.tblKomadNamestajas.Count(e => e.KomadNamestajaId == id) > 0;
        }
    }
}