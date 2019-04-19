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
    public class KategorijaNamestajasController : ApiController
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();

        // GET: api/KategorijaNamestajas
        public IQueryable<tblKategorijaNamestaja> GettblKategorijaNamestajas()
        {
            return db.tblKategorijaNamestajas;
        }

        // GET: api/KategorijaNamestajas/5
        [ResponseType(typeof(tblKategorijaNamestaja))]
        public IHttpActionResult GettblKategorijaNamestaja(int id)
        {
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            if (tblKategorijaNamestaja == null)
            {
                return NotFound();
            }

            return Ok(tblKategorijaNamestaja);
        }

        // PUT: api/KategorijaNamestajas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblKategorijaNamestaja(int id, tblKategorijaNamestaja tblKategorijaNamestaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblKategorijaNamestaja.KategroijaNamestajaId)
            {
                return BadRequest();
            }

            db.Entry(tblKategorijaNamestaja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblKategorijaNamestajaExists(id))
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

        // POST: api/KategorijaNamestajas
        [ResponseType(typeof(tblKategorijaNamestaja))]
        public IHttpActionResult PosttblKategorijaNamestaja(tblKategorijaNamestaja tblKategorijaNamestaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.tblKategorijaNamestajas.Add(tblKategorijaNamestaja);
                db.SaveChanges();
            }
            catch (Exception)
            {

                
                //return View();
            }
            

            return CreatedAtRoute("DefaultApi", new { id = tblKategorijaNamestaja.KategroijaNamestajaId }, tblKategorijaNamestaja);
        }

        // DELETE: api/KategorijaNamestajas/5
        [ResponseType(typeof(tblKategorijaNamestaja))]
        public IHttpActionResult DeletetblKategorijaNamestaja(int id)
        {
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            if (tblKategorijaNamestaja == null)
            {
                return NotFound();
            }
            try
            {
                db.tblKategorijaNamestajas.Remove(tblKategorijaNamestaja);
                db.SaveChanges();
            }
            catch (Exception)
            {

                
            }
            

            return Ok(tblKategorijaNamestaja);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblKategorijaNamestajaExists(int id)
        {
            return db.tblKategorijaNamestajas.Count(e => e.KategroijaNamestajaId == id) > 0;
        }
    }
}