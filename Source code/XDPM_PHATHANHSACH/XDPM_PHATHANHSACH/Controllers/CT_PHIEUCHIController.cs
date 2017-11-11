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
    public class CT_PHIEUCHIController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: CT_PHIEUCHI
        public ActionResult Index()
        {
            var cT_PHIEUCHI = db.CT_PHIEUCHI.Include(c => c.PHIEUCHI).Include(c => c.SACH);
            return View(cT_PHIEUCHI.ToList());
        }

        // GET: CT_PHIEUCHI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUCHI cT_PHIEUCHI = db.CT_PHIEUCHI.Find(id);
            if (cT_PHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEUCHI);
        }

        // GET: CT_PHIEUCHI/Create
        public ActionResult Create()
        {
            var pc = Session["phieuchi"] as XDPM_PHATHANHSACH.Models.PHIEUCHI;
            ViewBag.MASACH = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SACH.TENSACH");
            ViewBag.GIA = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "GIA");
            ViewBag.SLLAY = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLLAY");
            ViewBag.SLDABAN = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLDABAN");

            ViewBag.KNhapSach = 0;

            return View();
        }

        // POST: CT_PHIEUCHI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ICollection<CT_PHIEUCHI> ctpcs)
        {
            var pc = Session["phieuchi"] as XDPM_PHATHANHSACH.Models.PHIEUCHI;

            if (ModelState.IsValid)
            {
                if (ctpcs == null)
                {
                    ViewBag.MASACH = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SACH.TENSACH");
                    ViewBag.GIA = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "GIA");
                    ViewBag.SLLAY = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLLAY");
                    ViewBag.SLDABAN = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLDABAN");

                    ViewBag.KNhapSach = 1;

                    return View();
                }
                else
                {
                    pc.TONGTIEN = Convert.ToInt32(Request.Form["tongtien"]);

                    db.PHIEUCHIs.Add(pc);
                    db.SaveChanges();

                    int _maPC = db.PHIEUCHIs.Max(p => p.MAPC);

                    foreach (CT_PHIEUCHI ctpc in ctpcs)
                    {
                        ctpc.MAPC = _maPC;

                        SACH s = db.SACHes.Find(ctpc.MASACH);
                        ctpc.GIA = s.GIA;
                        ctpc.THANHTIEN = ctpc.SOLUONG * ctpc.GIA;

                        //cập nhật số lượng đã trả trong chi tiết đại lý
                        CT_NXB _ctnxb = db.CT_NXB.Where(d => d.MANXB == pc.MANXB).Single(m => m.MASACH == ctpc.MASACH);
                        _ctnxb.SLDABAN += ctpc.SOLUONG;

                        db.CT_PHIEUCHI.Add(ctpc);
                        db.Entry(_ctnxb).State = EntityState.Modified;
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index", "PHIEUCHIs");
                }
            }

            ViewBag.MASACH = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SACH.TENSACH");
            ViewBag.GIA = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "GIA");
            ViewBag.SLLAY = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLLAY");
            ViewBag.SLDABAN = new SelectList(db.CT_NXB.Include(s => s.SACH).Where(x => x.MANXB == pc.MANXB), "MASACH", "SLDABAN");

            ViewBag.KNhapSach = 0;

            return View();
        }

        // GET: CT_PHIEUCHI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUCHI cT_PHIEUCHI = db.CT_PHIEUCHI.Find(id);
            if (cT_PHIEUCHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPC = new SelectList(db.PHIEUCHIs, "MAPC", "MAPC", cT_PHIEUCHI.MAPC);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", cT_PHIEUCHI.MASACH);
            return View(cT_PHIEUCHI);
        }

        // POST: CT_PHIEUCHI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPC,MASACH,GIA,SOLUONG,THANHTIEN")] CT_PHIEUCHI cT_PHIEUCHI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_PHIEUCHI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPC = new SelectList(db.PHIEUCHIs, "MAPC", "MAPC", cT_PHIEUCHI.MAPC);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", cT_PHIEUCHI.MASACH);
            return View(cT_PHIEUCHI);
        }

        // GET: CT_PHIEUCHI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEUCHI cT_PHIEUCHI = db.CT_PHIEUCHI.Find(id);
            if (cT_PHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEUCHI);
        }

        // POST: CT_PHIEUCHI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PHIEUCHI cT_PHIEUCHI = db.CT_PHIEUCHI.Find(id);
            db.CT_PHIEUCHI.Remove(cT_PHIEUCHI);
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
