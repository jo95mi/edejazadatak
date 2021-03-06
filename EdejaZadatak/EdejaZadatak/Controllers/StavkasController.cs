﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EdejaZadatak.Models
{
    public class StavkasController : Controller
    {
        private BazaEdejaZadatakEntities db = new BazaEdejaZadatakEntities();

        // GET: Stavkas
        public ActionResult Index()
        {
            var stavkas = db.Stavkas.Include(s => s.Faktura);
            return View(stavkas.ToList());
        }

        // GET: Stavkas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = db.Stavkas.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // GET: Stavkas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stavkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id,[Bind(Include = "BrojFakture,RedniBroj,Kolicina,Cena,Ukupno")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                var temp = db.Stavkas.Where(a=> a.BrojFakture == id).FirstOrDefault();
                if(temp == null) {
                    stavka.RedniBroj = 1;
                }
                else
                {
                    var temp2 = db.Stavkas.Where(a => a.BrojFakture == id);
                    stavka.RedniBroj = temp2.Max(r => r.RedniBroj) + 1;
                }
                stavka.BrojFakture = id;
                stavka.Ukupno = stavka.Kolicina * stavka.Cena;
                db.Stavkas.Add(stavka);
                Faktura fak = db.Fakturas.Find(stavka.BrojFakture);
                fak.Ukupno += stavka.Ukupno;
                db.Entry(fak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Fakturas", new { id =stavka.BrojFakture });
            }

            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture", stavka.BrojFakture);
            return View(stavka);
        }

        // GET: Stavkas/Edit/5
        public ActionResult Edit(string br, int rb)
        {
            if (br == null || rb == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stavka = db.Stavkas.Where(r => r.BrojFakture == br);
            Stavka stavkaa = stavka.Where(a => a.RedniBroj == rb).FirstOrDefault();
            if (stavkaa == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture", stavkaa.BrojFakture);
            return View(stavkaa);
        }

        // POST: Stavkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrojFakture,RedniBroj,Kolicina,Cena,Ukupno")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                stavka.Ukupno = stavka.Kolicina * stavka.Cena;
                db.Entry(stavka).State = EntityState.Modified;
                db.SaveChanges();

                var stavkice = db.Stavkas.Where(r => r.BrojFakture == stavka.BrojFakture);
                decimal ukupnooo = 0;
                foreach (Stavka s in stavkice) {
                    ukupnooo += s.Ukupno;
                }
                Faktura f = db.Fakturas.Where(r => r.BrojFakture == stavka.BrojFakture).FirstOrDefault();
                f.Ukupno = ukupnooo;
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Details", "Fakturas", new { id = stavka.BrojFakture });
            }
            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture", stavka.BrojFakture);
            return View(stavka);
        }

        // GET: Stavkas/Delete/5
        public ActionResult Delete(string br, int rb)
        {
            if (br == null || rb == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stavka = db.Stavkas.Where(r => r.BrojFakture == br);
            Stavka stavkaa = stavka.Where(a => a.RedniBroj == rb).FirstOrDefault();
            if (stavkaa == null)
            {
                return HttpNotFound();
            }
            return View(stavkaa);
        }

        // POST: Stavkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string br, int rb)
        {
            var stavka = db.Stavkas.Where(r => r.BrojFakture == br);
            Stavka stavkaa = stavka.Where(a => a.RedniBroj == rb).FirstOrDefault();
            db.Stavkas.Remove(stavkaa);
            Faktura fak = db.Fakturas.Find(stavkaa.BrojFakture);
            fak.Ukupno = fak.Ukupno - stavkaa.Ukupno;
            db.Entry(fak).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details","Fakturas", new { id = br});
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
