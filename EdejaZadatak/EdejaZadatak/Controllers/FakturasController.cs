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
    public class FakturasController : Controller
    {
        private BazaEdejaZadatakEntities db = new BazaEdejaZadatakEntities();

        // GET: Fakturas
        public ActionResult Index()
        {
            return View(db.Fakturas.ToList());
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakturas.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fakturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrojFakture,DatumFakture,Ukupno")] Faktura faktura)
        {
            DateTime dan = DateTime.Today;
            faktura.DatumFakture = dan.Date;
            if (ModelState.IsValid)
            {
                db.Fakturas.Add(faktura);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = faktura.BrojFakture });
            }

            return View(faktura);
        }
        // GET: Fakturas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Fakturas.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Faktura faktura = db.Fakturas.Find(id);
            var stavke = db.Stavkas.Where(r => r.BrojFakture == id);
            db.Stavkas.RemoveRange(stavke);
            db.Fakturas.Remove(faktura);
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