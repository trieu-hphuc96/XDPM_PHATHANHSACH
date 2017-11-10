using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using XDPM_PHATHANHSACH.Models;

namespace XDPM_PHATHANHSACH.Controllers
{
    public class PHIEUNHAPsController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: PHIEUNHAPs
        public ActionResult Index()
        {
            var pHIEUNHAPs = db.PHIEUNHAPs.Include(p => p.NXB);
            return View(pHIEUNHAPs.ToList());
        }

        // GET: PHIEUNHAPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAP);
        }

        // GET: PHIEUNHAPs/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            ViewBag.KNhapSach = 0;
            return View();
        }

        // POST: PHIEUNHAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(PHIEUNHAP pn, ICollection<CT_PHIEUNHAP> ctpns)
        {
            if (ModelState.IsValid)
            {
                if (ctpns == null)
                {
                    ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
                    ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
                    ViewBag.KNhapSach = 1;
                    return View();
                }

                db.PHIEUNHAPs.Add(pn);
                db.SaveChanges();

                int ma = db.PHIEUNHAPs.Max(p => p.MAPN);
                int tongtien = 0;
                
                foreach (CT_PHIEUNHAP u in ctpns)
                {
                    if (u != null)
                    {
                        u.MAPN = ma;
                        SACH a = db.SACHes.Find(u.MASACH);
                        u.GIA = a.GIA;
                        u.THANHTIEN = u.SOLUONG * u.GIA;
                        a.SOLUONG = a.SOLUONG + u.SOLUONG;
                        tongtien = tongtien + u.SOLUONG * a.GIA;

                        db.CT_PHIEUNHAP.Add(u);
                        db.Entry(a).State = EntityState.Modified;
                    }
                }

                PHIEUNHAP pn_tam = db.PHIEUNHAPs.Find(ma);
                pn_tam.TONGTIEN = tongtien;
                db.Entry(pn_tam).State = EntityState.Modified;

                NXB n = db.NXBs.Find(pn.MANXB);
                n.TIENCONNO = n.TIENCONNO + tongtien;
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            ViewBag.KNhapSach = 0;
            return View();
        }

        // GET: PHIEUNHAPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
            return View(pHIEUNHAP);
        }

        // POST: PHIEUNHAPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPN,MANXB,NGAYNHAP,NGUOIGIAO,TONGTIEN")] PHIEUNHAP pHIEUNHAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
            return View(pHIEUNHAP);
        }

        // GET: PHIEUNHAPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAP);
        }

        // POST: PHIEUNHAPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            db.PHIEUNHAPs.Remove(pHIEUNHAP);
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
