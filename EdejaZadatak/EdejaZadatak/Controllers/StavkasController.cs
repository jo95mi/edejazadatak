using System;
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
            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture");
            return View();
        }

        // POST: Stavkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrojFakture,RedniBroj,Kolicina,Cena,Ukupno")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Stavkas.Add(stavka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture", stavka.BrojFakture);
            return View(stavka);
        }

        // GET: Stavkas/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.BrojFakture = new SelectList(db.Fakturas, "BrojFakture", "BrojFakture", stavka.BrojFakture);
            return View(stavka);
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
                db.Entry(stavka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
