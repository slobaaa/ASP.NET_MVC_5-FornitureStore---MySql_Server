using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles;

namespace shanuMVCUserRoles.Controllers
{
    
    public class tblKategorijaNamestajasController : Controller
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();

        // GET: tblKategorijaNamestajas
        [Authorize]
        public ActionResult Index()
        {
            return View(db.tblKategorijaNamestajas.ToList());
        }

        // GET: tblKategorijaNamestajas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            if (tblKategorijaNamestaja == null)
            {
                return HttpNotFound();
            }
            return View(tblKategorijaNamestaja);
        }

        // GET: tblKategorijaNamestajas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblKategorijaNamestajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "KategroijaNamestajaId,Naziv,Opis")] tblKategorijaNamestaja tblKategorijaNamestaja)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.tblKategorijaNamestajas.Add(tblKategorijaNamestaja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msgKategorijaNamestaja"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                }
                
            }

            return View(tblKategorijaNamestaja);
        }

        // GET: tblKategorijaNamestajas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            if (tblKategorijaNamestaja == null)
            {
                return HttpNotFound();
            }
            return View(tblKategorijaNamestaja);
        }

        // POST: tblKategorijaNamestajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "KategroijaNamestajaId,Naziv,Opis")] tblKategorijaNamestaja tblKategorijaNamestaja)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tblKategorijaNamestaja).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msgKategorijaNamestaja"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                }
                
            }
            return View(tblKategorijaNamestaja);
        }

        // GET: tblKategorijaNamestajas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            if (tblKategorijaNamestaja == null)
            {
                return HttpNotFound();
            }
            return View(tblKategorijaNamestaja);
        }

        // POST: tblKategorijaNamestajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblKategorijaNamestaja tblKategorijaNamestaja = db.tblKategorijaNamestajas.Find(id);
            db.tblKategorijaNamestajas.Remove(tblKategorijaNamestaja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
