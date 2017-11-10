using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XDPM_PHATHANHSACH.Models;

namespace XDPM_PHATHANHSACH.Controllers
{
    public class SACHesController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: SACHes
        public ActionResult Index()
        {
            var sACHes = db.SACHes.Include(s => s.NXB);
            return View(sACHes.ToList());
        }

        // GET: SACHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // GET: SACHes/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            return View();
        }

        // POST: SACHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASACH,MANXB,TENSACH,TACGIA,THELOAI,SOLUONG,GIA")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.SACHes.Add(sACH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", sACH.MANXB);
            return View(sACH);
        }

        // GET: SACHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", sACH.MANXB);
            return View(sACH);
        }

        // POST: SACHes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASACH,MANXB,TENSACH,TACGIA,THELOAI,SOLUONG,GIA")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", sACH.MANXB);
            return View(sACH);
        }

        // GET: SACHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // POST: SACHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH sACH = db.SACHes.Find(id);
            db.SACHes.Remove(sACH);
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

        public ActionResult ThongKeTonKho()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                string query = "select s.*, 0 + soluongnhap- soluongxuat as soluongton "
                            + " from SACH s, (select s.MASACH, sum(ctpn.SOLUONG) as soluongnhap "
                                        + "from sach s, PHIEUNHAP pn, CT_PHIEUNHAP ctpn "
                                        + "where NGAYNHAP = '" + DateTime.Today + "' and pn.MAPN=ctpn.MAPN and ctpn.MASACH = s.MASACH "
                                        + "group by s.MASACH) as slnhap, "
                                        + "(select s.MASACH, sum(ctpn.SOLUONG) as soluongxuat "
                                        + "from sach s, PHIEUXUAT pn, CT_PHIEUXUAT ctpn "
                                        + "where NGAYXUAT = '" + DateTime.Today + "' and pn.MAPX=ctpn.MAPX and ctpn.MASACH = s.MASACH "
                                        + "group by s.MASACH) as slxuat "
                            + "where s.MASACH = slnhap.MASACH and s.MASACH = slxuat.MASACH";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            ViewBag.ngay = DateTime.Today;
            return View(dt);
        }

        [HttpPost]
        public ActionResult ThongKeTonKho(string datepicker)
        {
            DataTable dt = new DataTable();
            if (datepicker != null)
                using (SqlConnection con = DBConnection.connectDB())
                {
                    string query = "select s.*, 0 + soluongnhap- soluongxuat as soluongton "
                                + " from SACH s, (select s.MASACH, sum(ctpn.SOLUONG) as soluongnhap "
                                            + "from sach s, PHIEUNHAP pn, CT_PHIEUNHAP ctpn "
                                            + "where NGAYNHAP = '" + datepicker + "' and pn.MAPN=ctpn.MAPN and ctpn.MASACH = s.MASACH "
                                            + "group by s.MASACH) as slnhap, "
                                            + "(select s.MASACH, sum(ctpn.SOLUONG) as soluongxuat "
                                            + "from sach s, PHIEUXUAT pn, CT_PHIEUXUAT ctpn "
                                            + "where NGAYXUAT = '" + datepicker + "' and pn.MAPX=ctpn.MAPX and ctpn.MASACH = s.MASACH "
                                            + "group by s.MASACH) as slxuat "
                                + "where s.MASACH = slnhap.MASACH and s.MASACH = slxuat.MASACH";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                        }
                    }
                }
            ViewBag.ngay = datepicker;
            return View(dt);
        }
    }
}
