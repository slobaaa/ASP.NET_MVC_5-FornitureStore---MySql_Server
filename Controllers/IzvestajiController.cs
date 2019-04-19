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
    public class IzvestajiController : Controller
    {
        private ProdavnicaNamestajaEntities db = new ProdavnicaNamestajaEntities();



        //moj kontroler da ispise kategoriju namestaja u izvestajima
        public ActionResult ispisiKategorijaNamestajaIzvestajDropdown()
        {
            return Json(db.tblKategorijaNamestajas.Select(x => new
            {
                KategroijaNamestajaId = x.KategroijaNamestajaId,
                Naziv = x.Naziv
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //moj kontroler da ispise kategoriju namestaja u izvestajima
        public ActionResult getRacunIzvestaj()
        {
            return Json(db.tblRacuns.Select(x => new
            {
                RacunId = x.RacunId,
                KomadNamestajaId = x.KomadNamestajaId,
                Kolicina = x.Kolicina,
                CenaBezPdv = x.CenaBezPdv,
                Porez = x.Porez,
                UkupnaCenaSaPorezom = x.UkupnaCenaSaPorezom,
                DatumIVremeKupovine = x.DatumIVremeKupovine,
                SalonId = x.SalonId
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //moj kontroler da ispise komade namestaja u kategroji u racunima IZVESTAJ
        public ActionResult getKomadNamestajaPoKategoriji()
        {
            return Json(db.tblKomadNamestajas.Select(x => new
            {
                KomadNamestajaId = x.KomadNamestajaId,
                Naziv = x.Naziv,
                KategorijaNamestajaId = x.KategorijaNamestajaId
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Izvestaji
        public ActionResult Index()
        {
            return View();
        }
    }
}