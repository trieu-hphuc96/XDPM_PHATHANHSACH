using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XDPM_PHATHANHSACH.Models;

namespace XDPM_PHATHANHSACH.Controllers
{
    public class PHIEUCHIsController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: PHIEUCHIs
        public ActionResult Index()
        {
            var pHIEUCHIs = db.PHIEUCHIs.Include(p => p.NXB);
            return View(pHIEUCHIs.ToList());
        }

        // GET: PHIEUCHIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            return View();
        }

        // POST: PHIEUCHIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPC,MANXB,NGAY,TONGTIEN")] PHIEUCHI pHIEUCHI)
        {
            if (ModelState.IsValid)
            {
                Session["phieuchi"] = pHIEUCHI;
                return RedirectToAction("Create","CT_PHIEUCHI");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // POST: PHIEUCHIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPC,MANXB,NGAY,TONGTIEN")] PHIEUCHI pHIEUCHI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUCHI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUCHI);
        }

        // POST: PHIEUCHIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            db.PHIEUCHIs.Remove(pHIEUCHI);
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
