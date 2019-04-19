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
    public class tblSalonsController : Controller
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();


        //moj kontroler za ispis u dropdown salone za neprijavljene korisnike
        public ActionResult getSalons()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblSalons.Select(x => new
            {
                SalonId = x.SalonId,
                Naziv = x.Naziv,
     
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //moj kontroler za ispis u listu nakon izbora dropdown-a
        public ActionResult getNamestajPosleIzboraSalona()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblKomadNamestajas.Select(x => new
            {
                KomadNamestajaId = x.KomadNamestajaId,
                SalonId = x.SalonId,
                Naziv = x.Naziv,
                KolicinaUMagacinu = x.KolicinaUMagacinu,
                JedinicnaCena = x.JedinicnaCena,
                Slika = x.Slika
            }).ToList(), JsonRequestBehavior.AllowGet);
        }








        // GET: tblSalons
        public ActionResult Index()
        {
            return View(db.tblSalons.ToList());
        }

        // GET: tblSalons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSalon tblSalon = db.tblSalons.Find(id);
            if (tblSalon == null)
            {
                return HttpNotFound();
            }
            return View(tblSalon);
        }

        // GET: tblSalons/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblSalons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "SalonId,Naziv,Vlasnik,Adresa,Telefon,Email,WebStranica,PIB,BrojZiroRacuna")] tblSalon tblSalon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.tblSalons.Add(tblSalon);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msg"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                    //return RedirectToAction("/");
                }
                
            }

            return View(tblSalon);
        }

        // GET: tblSalons/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSalon tblSalon = db.tblSalons.Find(id);
            if (tblSalon == null)
            {
                return HttpNotFound();
            }
            return View(tblSalon);
        }

        // POST: tblSalons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "SalonId,Naziv,Vlasnik,Adresa,Telefon,Email,WebStranica,PIB,BrojZiroRacuna")] tblSalon tblSalon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tblSalon).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    TempData["msg"] = "<script>alert('***GRESKA*** Uneli ste dupli podatak, pokusajte ponovo');</script>";
                    return View();
                }
                
            }
            return View(tblSalon);
        }

        // GET: tblSalons/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSalon tblSalon = db.tblSalons.Find(id);
            if (tblSalon == null)
            {
                return HttpNotFound();
            }
            return View(tblSalon);
        }

        // POST: tblSalons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tblSalon tblSalon = db.tblSalons.Find(id);
                db.tblSalons.Remove(tblSalon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
            
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
