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
    public class tblKomadNamestajasController : Controller
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();


        //moj kontroler za sliku
        public ActionResult AddImage()
        {
            tblKomadNamestaja k1 = new tblKomadNamestaja();
            return View(k1);
        }
        [HttpPost]
        public ActionResult AddImage(tblKomadNamestaja model, HttpPostedFileBase image1)
        {
            var db = new ProdavnicaNamestajaEntities();
            if (image1!=null)
            {
                model.Slika = new byte[image1.ContentLength];
                image1.InputStream.Read(model.Slika, 0, image1.ContentLength);
            }
            db.tblKomadNamestajas.Add(model);
            db.SaveChanges();
            return View(model);
        }
        //kraj mog kontrolera za sliku


        // GET: tblKomadNamestajas
        public ActionResult Index()
        {
            var tblKomadNamestajas = db.tblKomadNamestajas.Include(t => t.tblKategorijaNamestaja).Include(t => t.tblSalon);
            return View(tblKomadNamestajas.ToList());
        }

        // GET: tblKomadNamestajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            if (tblKomadNamestaja == null)
            {
                return HttpNotFound();
            }
            return View(tblKomadNamestaja);
        }

        // GET: tblKomadNamestajas/Create
        public ActionResult Create()
        {
            ViewBag.KategorijaNamestajaId = new SelectList(db.tblKategorijaNamestajas, "KategroijaNamestajaId", "Naziv");
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv");
            return View();
        }

        // POST: tblKomadNamestajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "KomadNamestajaId,Sifra,Naziv,ZemljaProizvodnje,GodinaProizvodnje,JedinicnaCena,KolicinaUMagacinu,SalonId,KategorijaNamestajaId,Slika")] tblKomadNamestaja tblKomadNamestaja)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.tblKomadNamestajas.Add(tblKomadNamestaja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msgKomadNamestaja"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                }
                
            }

            ViewBag.KategorijaNamestajaId = new SelectList(db.tblKategorijaNamestajas, "KategroijaNamestajaId", "Naziv", tblKomadNamestaja.KategorijaNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblKomadNamestaja.SalonId);
            return View(tblKomadNamestaja);
        }

        // GET: tblKomadNamestajas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            if (tblKomadNamestaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaNamestajaId = new SelectList(db.tblKategorijaNamestajas, "KategroijaNamestajaId", "Naziv", tblKomadNamestaja.KategorijaNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblKomadNamestaja.SalonId);
            return View(tblKomadNamestaja);
        }

        // POST: tblKomadNamestajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "KomadNamestajaId,Sifra,Naziv,ZemljaProizvodnje,GodinaProizvodnje,JedinicnaCena,KolicinaUMagacinu,SalonId,KategorijaNamestajaId,Slika")] tblKomadNamestaja tblKomadNamestaja)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tblKomadNamestaja).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msgKomadNamestaja"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                }
                
            }
            ViewBag.KategorijaNamestajaId = new SelectList(db.tblKategorijaNamestajas, "KategroijaNamestajaId", "Naziv", tblKomadNamestaja.KategorijaNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblKomadNamestaja.SalonId);
            return View(tblKomadNamestaja);
        }

        // GET: tblKomadNamestajas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            if (tblKomadNamestaja == null)
            {
                return HttpNotFound();
            }
            return View(tblKomadNamestaja);
        }

        // POST: tblKomadNamestajas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblKomadNamestaja tblKomadNamestaja = db.tblKomadNamestajas.Find(id);
            db.tblKomadNamestajas.Remove(tblKomadNamestaja);
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
