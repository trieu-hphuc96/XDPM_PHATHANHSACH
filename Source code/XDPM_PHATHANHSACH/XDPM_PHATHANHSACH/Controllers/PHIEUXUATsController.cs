using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using XDPM_PHATHANHSACH.Models;

namespace XDPM_PHATHANHSACH.Controllers
{
    public class PHIEUXUATsController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: PHIEUXUATs
        public ActionResult Index()
        {
            var pHIEUXUATs = db.PHIEUXUATs.Include(p => p.DAILY);
            return View(pHIEUXUATs.ToList());
        }

        // GET: PHIEUXUATs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            ViewBag.SOLUONG = new SelectList(db.SACHes, "MASACH", "SOLUONG");
            ViewBag.GIA = new SelectList(db.SACHes, "MASACH", "GIA");

            ViewBag.XuatQuaNhieu = 0;
            ViewBag.KNhapSach = 0;

            return View();
        }

        // POST: PHIEUXUATs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( PHIEUXUAT px, ICollection<CT_PHIEUXUAT> ctpxs)
        {
            if (ModelState.IsValid)
            {
                int tongtien = CT_PHIEUXUAT.tinhTongTienPhieuXuat(ctpxs, db.SACHes);

                if (ctpxs == null) //kiểm tra nghiệp vụ
                {
                    ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
                    ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
                    ViewBag.SOLUONG = new SelectList(db.SACHes, "MASACH", "SOLUONG");
                    ViewBag.GIA = new SelectList(db.SACHes, "MASACH", "GIA");

                    ViewBag.XuatQuaNhieu = 0;
                    ViewBag.KNhapSach = 1;

                    return View();
                }
                else if (tongtien > 10000000)
                {
                    ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
                    ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
                    ViewBag.SOLUONG = new SelectList(db.SACHes, "MASACH", "SOLUONG");
                    ViewBag.GIA = new SelectList(db.SACHes, "MASACH", "GIA");

                    ViewBag.XuatQuaNhieu = 1;
                    ViewBag.KNhapSach = 0;
                    return View();
                }
                else //thêm phiếu xuất và chi tiết phiếu xuất
                {
                    db.PHIEUXUATs.Add(px);
                    db.SaveChanges();

                    int ma = db.PHIEUXUATs.Max(p => p.MAPX);
                    foreach (CT_PHIEUXUAT ctpx in ctpxs)
                    {
                        if (ctpx != null)
                        {
                            ctpx.MAPX = ma;
                            SACH s = db.SACHes.Find(ctpx.MASACH);
                            ctpx.GIA = s.GIA;
                            ctpx.THANHTIEN = ctpx.SOLUONG * ctpx.GIA;
                            s.SOLUONG = s.SOLUONG - ctpx.SOLUONG;

                            CT_DAILY ctdl;
                            bool check = db.CT_DAILY.Any(re => re.MADL.Equals(px.MADL) && re.MASACH.Equals(ctpx.MASACH));
                            if (!check)
                            {
                                ctdl = new CT_DAILY();
                                ctdl.MADL = px.MADL;
                                ctdl.MASACH = ctpx.MASACH;
                                ctdl.GIA = ctpx.GIA;
                                ctdl.SLLAY = ctpx.SOLUONG;
                                ctdl.SLDABAN = 0;

                                db.CT_DAILY.Add(ctdl);
                            }
                            else
                            {
                                ctdl = db.CT_DAILY.Where(re => re.MADL == px.MADL).Single(re2 => re2.MASACH == ctpx.MASACH);
                                ctdl.SLLAY += ctpx.SOLUONG;
                                db.Entry(ctdl).State = EntityState.Modified;
                            }

                            db.CT_PHIEUXUAT.Add(ctpx);
                            db.Entry(s).State = EntityState.Modified;
                        }
                    }

                    //thêm tổng tiền phiếu xuất và cập nhật tiền nợ đại lý
                    PHIEUXUAT px_tam = db.PHIEUXUATs.Find(ma);
                    px_tam.TONGTIEN = tongtien;
                    db.Entry(px_tam).State = EntityState.Modified;

                    DAILY d = db.DAILies.Find(px.MADL);
                    d.TIENCONNO = d.TIENCONNO + tongtien;
                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            ViewBag.SOLUONG = new SelectList(db.SACHes, "MASACH", "SOLUONG");
            ViewBag.GIA = new SelectList(db.SACHes, "MASACH", "GIA");
            ViewBag.XuatQuaNhieu = 0;
            ViewBag.KNhapSach = 0;
            return View();
        }

        // GET: PHIEUXUATs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // POST: PHIEUXUATs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPX,MADL,NGAYXUAT,NGUOINHAN,TONGTIEN")] PHIEUXUAT pHIEUXUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUXUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUXUAT);
        }

        // POST: PHIEUXUATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            db.PHIEUXUATs.Remove(pHIEUXUAT);
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
