using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles;
using Microsoft.Reporting.WebForms;

namespace shanuMVCUserRoles.Controllers
{
    public class tblRacunsController : Controller
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();


        //moj kontroler da upise cenu u racune iz dropdown-a
        public ActionResult getJedinicnaCenaBezPdv()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblKomadNamestajas.Select(x => new
            {
                KomadNamestajaId = x.KomadNamestajaId,
                JedinicnaCena = x.JedinicnaCena,
                SalonId = x.SalonId
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //moj kontroler da upise cenu u racune iz dropdown-a
        public ActionResult getBrojRacunaIzBaze()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblRacuns.Select(x => new
            {
                RacunId = x.RacunId,
                BrojRacuna = x.BrojRacuna,
                KomadNamestajaId = x.KomadNamestajaId,
                Kolicina = x.Kolicina
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        //posle izbora salona u racunu izlista proizvode tamo
        public ActionResult getNamestajPosleIzboraSalona()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblKomadNamestajas.Select(x => new
            {
                KomadNamestajaId = x.KomadNamestajaId,
                SalonId = x.SalonId,
                Naziv= x.Naziv
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        //ZA UPDATE TRUE NA FALSE ZAVRSENOG RECORDA
        public ActionResult updateZavrsenRecord()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblRacuns.Select(x => new
            {
                RacunId = x.RacunId,
                KomadNamestajaId = x.KomadNamestajaId,
                Porez = x.Porez,
                CenaBezPdv = x.CenaBezPdv,
                UkupnaCenaSaPorezom =x.UkupnaCenaSaPorezom,
                DatumIVremeKupovine =x.DatumIVremeKupovine,
                KupacKojiJeKupioRobu = x.KupacKojiJeKupioRobu,
                SalonId = x.SalonId,
                BrojRacuna = x.BrojRacuna,
                Uknjizeno = x.Uknjizeno
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        //KOD ZAPOCINJANJA RACUNA DA IZBRISE RECORDE AKO NIJE ZAVRSEN RACUN
        public ActionResult deleteAkoNijeZavrsenRacun()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblRacuns.Select(x => new
            {
                RacunId = x.RacunId,
                BrojRacuna=x.BrojRacuna,
                Uknjizeno = x.Uknjizeno
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        //da pri dodatom prozivodu smanji komad namestaja iz magacina
        public ActionResult smanjiBrojKomadaIzMagacina()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblKomadNamestajas.Select(x => new
            {
                KomadNamestajaId = x.KomadNamestajaId,
                Sifra = x.Sifra,
                Naziv=x.Naziv,
                ZemljaProizvodnje = x.ZemljaProizvodnje,
                GodinaProizvodnje = x.GodinaProizvodnje,
                JedinicnaCena = x.JedinicnaCena,
                KolicinaUMagacinu = x.KolicinaUMagacinu,
                SalonId = x.SalonId,
                KategorijaNamestajaId = x.KategorijaNamestajaId,
                Slika = x.Slika
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult getRacunZaAdmina()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.tblRacuns.Select(x => new
            {
                RacunId = x.RacunId,
                KomadNamestajaId = x.KomadNamestajaId,
                Porez = x.Porez,
                CenaBezPdv = x.CenaBezPdv,
                UkupnaCenaSaPorezom = x.UkupnaCenaSaPorezom,
                DatumIVremeKupovine = x.DatumIVremeKupovine,
                KupacKojiJeKupioRobu = x.KupacKojiJeKupioRobu,
                SalonId = x.SalonId,
                BrojRacuna = x.BrojRacuna,
                Uknjizeno = x.Uknjizeno,
                Kolicina = x.Kolicina
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        //Kontroler za report racuna
        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Racun.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "RacunDataSet";
            reportDataSource.Value = db.tblRacuns.ToList();
            localreport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType =="Pdf")
            {
                fileNameExtension = "pdf";
            }
            if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            if (reportType == "Image")
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment: fileName = racun_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
            //return View();
        }






        public ActionResult Index()
        {
            var tblRacuns = db.tblRacuns.Include(t => t.tblKomadNamestaja).Include(t => t.tblSalon);
            return View(tblRacuns.ToList());
        }

        // GET: tblRacuns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRacun tblRacun = db.tblRacuns.Find(id);
            if (tblRacun == null)
            {
                return HttpNotFound();
            }
            return View(tblRacun);
        }

        // GET: tblRacuns/Create
        public ActionResult Create()
        {
            ViewBag.KomadNamestajaId = new SelectList(db.tblKomadNamestajas, "KomadNamestajaId", "Sifra");
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv");
            return View();
        }

        // POST: tblRacuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunId,KomadNamestajaId,Porez,UkupnaCenaSaPorezom,DatumIVremeKupovine,KupacKojiJeKupioRobu,SalonId,CenaBezPdv,BrojRacuna,Uknjizeno,Kolicina")] tblRacun tblRacun)
        {
            if (ModelState.IsValid)
            {
                db.tblRacuns.Add(tblRacun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KomadNamestajaId = new SelectList(db.tblKomadNamestajas, "KomadNamestajaId", "Sifra", tblRacun.KomadNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblRacun.SalonId);
            return View(tblRacun);
        }

        // GET: tblRacuns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRacun tblRacun = db.tblRacuns.Find(id);
            if (tblRacun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KomadNamestajaId = new SelectList(db.tblKomadNamestajas, "KomadNamestajaId", "Sifra", tblRacun.KomadNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblRacun.SalonId);
            return View(tblRacun);
        }

        // POST: tblRacuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacunId,KomadNamestajaId,Porez,UkupnaCenaSaPorezom,DatumIVremeKupovine,KupacKojiJeKupioRobu,SalonId,CenaBezPdv,BrojRacuna,Uknjizeno,Kolicina")] tblRacun tblRacun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRacun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KomadNamestajaId = new SelectList(db.tblKomadNamestajas, "KomadNamestajaId", "Sifra", tblRacun.KomadNamestajaId);
            ViewBag.SalonId = new SelectList(db.tblSalons, "SalonId", "Naziv", tblRacun.SalonId);
            return View(tblRacun);
        }

        // GET: tblRacuns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRacun tblRacun = db.tblRacuns.Find(id);
            if (tblRacun == null)
            {
                return HttpNotFound();
            }
            return View(tblRacun);
        }

        // POST: tblRacuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRacun tblRacun = db.tblRacuns.Find(id);
            db.tblRacuns.Remove(tblRacun);
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
