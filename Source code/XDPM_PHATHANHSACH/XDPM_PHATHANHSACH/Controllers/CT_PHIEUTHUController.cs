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
    public class CT_PHIEUTHUController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: CT_PHIEUTHU
        public ActionResult Index()
        {
            var cT_PHIEUTHU = db.CT_PHIEUTHU.Include(c => c.PHIEUTHU).Include(c => c.SACH);
            return View(cT_PHIEUTHU.ToList());
        }

        // GET: CT_PHIEUTHU/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUTHU cT_PHIEUTHU = db.CT_PHIEUTHU.Find(id);
            if (cT_PHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEUTHU);
        }

        // GET: CT_PHIEUTHU/Create
        public ActionResult Create()
        {
            var a = Session["phieuthu"] as XDPM_PHATHANHSACH.Models.PHIEUTHU;
            ViewBag.MASACH = new SelectList(db.CT_DAILY.Include(s=>s.SACH).Where(x=>x.MADL == a.MADL), "MASACH", "SACH.TENSACH");
            ViewBag.GIA = new SelectList(db.CT_DAILY.Include(s => s.SACH).Where(x => x.MADL == a.MADL), "MASACH", "GIA");
            ViewBag.SLLAY = new SelectList(db.CT_DAILY.Include(s => s.SACH).Where(x => x.MADL == a.MADL), "MASACH", "SLLAY");
            ViewBag.SLDABAN = new SelectList(db.CT_DAILY.Include(s => s.SACH).Where(x => x.MADL == a.MADL), "MASACH", "SLDABAN");

            return View();
        }

        // POST: CT_PHIEUTHU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPT,MASACH,GIA,SOLUONG,THANHTIEN")] CT_PHIEUTHU cT_PHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.CT_PHIEUTHU.Add(cT_PHIEUTHU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPT = new SelectList(db.PHIEUTHUs, "MAPT", "MAPT", cT_PHIEUTHU.MAPT);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", cT_PHIEUTHU.MASACH);
            return View(cT_PHIEUTHU);
        }

        // GET: CT_PHIEUTHU/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUTHU cT_PHIEUTHU = db.CT_PHIEUTHU.Find(id);
            if (cT_PHIEUTHU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPT = new SelectList(db.PHIEUTHUs, "MAPT", "MAPT", cT_PHIEUTHU.MAPT);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", cT_PHIEUTHU.MASACH);
            return View(cT_PHIEUTHU);
        }

        // POST: CT_PHIEUTHU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPT,MASACH,GIA,SOLUONG,THANHTIEN")] CT_PHIEUTHU cT_PHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_PHIEUTHU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPT = new SelectList(db.PHIEUTHUs, "MAPT", "MAPT", cT_PHIEUTHU.MAPT);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", cT_PHIEUTHU.MASACH);
            return View(cT_PHIEUTHU);
        }

        // GET: CT_PHIEUTHU/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUTHU cT_PHIEUTHU = db.CT_PHIEUTHU.Find(id);
            if (cT_PHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEUTHU);
        }

        // POST: CT_PHIEUTHU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PHIEUTHU cT_PHIEUTHU = db.CT_PHIEUTHU.Find(id);
            db.CT_PHIEUTHU.Remove(cT_PHIEUTHU);
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
