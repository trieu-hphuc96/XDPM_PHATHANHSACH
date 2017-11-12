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
using XDPM_PHATHANHSACH.ViewModels;

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
                SqlCommand cmd = new SqlCommand("sp_layThongKeTonKho", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ngay", SqlDbType.Date);

                cmd.Parameters["@ngay"].Value = DateTime.Today;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            ViewBag.ngay = DateTime.Today.ToString("dd/MM/yyyy");

            return View(dt);
        }

        [HttpPost]
        public ActionResult ThongKeTonKho(string datepicker)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_layThongKeTonKho", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ngay", SqlDbType.Date);

                cmd.Parameters["@ngay"].Value = DateTime.ParseExact(datepicker, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            ViewBag.ngay = datepicker;

            return View(dt);
        }

        public ActionResult thongkeDoanhThu()
        {
            PHIEUTHU_PHIEUCHI _ptpc = new PHIEUTHU_PHIEUCHI();
            _ptpc.pt = new DataTable();
            _ptpc.pc = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_layPhieuThu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tungay", SqlDbType.Date);
                cmd.Parameters.Add("@denngay", SqlDbType.Date);

                cmd.Parameters["@tungay"].Value = DateTime.Today;
                cmd.Parameters["@denngay"].Value = DateTime.Today;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(_ptpc.pt);
                }

                cmd = new SqlCommand("sp_layPhieuChi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tungay", SqlDbType.Date);
                cmd.Parameters.Add("@denngay", SqlDbType.Date);

                cmd.Parameters["@tungay"].Value = DateTime.Today;
                cmd.Parameters["@denngay"].Value = DateTime.Today;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(_ptpc.pc);
                }
            }

            ViewBag.tungay = DateTime.Today.ToString("dd/MM/yyyy");
            ViewBag.denngay = DateTime.Today.ToString("dd/MM/yyyy");

            int TongThu = 0;
            foreach (DataRow dr in _ptpc.pt.Rows)
            {
                TongThu += dr.Field<int>("TienThu");
            }
            int TongChi = 0;
            foreach (DataRow dr in _ptpc.pc.Rows)
            {
                TongChi += dr.Field<int>("TienChi");
            }

            ViewBag.TongThu = TongThu;
            ViewBag.TongChi = TongChi;
            ViewBag.LoiNhuan = TongThu - TongChi;
            ViewBag.LoiNgay = 0;

            return View(_ptpc);
        }

        [HttpPost]
        public ActionResult thongkeDoanhThu(string tungay, string denngay)
        {
            PHIEUTHU_PHIEUCHI _ptpc = new PHIEUTHU_PHIEUCHI();
            _ptpc.pt = new DataTable();
            _ptpc.pc = new DataTable();
            int TongThu = 0;
            int TongChi = 0;

            if (DateTime.ParseExact(tungay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) > DateTime.ParseExact(denngay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            {
                using (SqlConnection con = DBConnection.connectDB())
                {
                    SqlCommand cmd = new SqlCommand("sp_layPhieuThu", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tungay", SqlDbType.Date);
                    cmd.Parameters.Add("@denngay", SqlDbType.Date);

                    cmd.Parameters["@tungay"].Value = DateTime.Today;
                    cmd.Parameters["@denngay"].Value = DateTime.Today;

                    cmd.ExecuteNonQuery();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(_ptpc.pt);
                    }

                    cmd = new SqlCommand("sp_layPhieuChi", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tungay", SqlDbType.Date);
                    cmd.Parameters.Add("@denngay", SqlDbType.Date);

                    cmd.Parameters["@tungay"].Value = DateTime.Today;
                    cmd.Parameters["@denngay"].Value = DateTime.Today;

                    cmd.ExecuteNonQuery();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(_ptpc.pc);
                    }
                }

                ViewBag.tungay = DateTime.Today.ToString("dd/MM/yyyy");
                ViewBag.denngay = DateTime.Today.ToString("dd/MM/yyyy");

                TongThu = 0;
                foreach (DataRow dr in _ptpc.pt.Rows)
                {
                    TongThu += dr.Field<int>("TienThu");
                }
                TongChi = 0;
                foreach (DataRow dr in _ptpc.pc.Rows)
                {
                    TongChi += dr.Field<int>("TienChi");
                }

                ViewBag.TongThu = TongThu;
                ViewBag.TongChi = TongChi;
                ViewBag.LoiNhuan = TongThu - TongChi;
                ViewBag.LoiNgay = 1;

                return View(_ptpc);
            }
            else
            {
                using (SqlConnection con = DBConnection.connectDB())
                {
                    SqlCommand cmd = new SqlCommand("sp_layPhieuThu", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tungay", SqlDbType.Date);
                    cmd.Parameters.Add("@denngay", SqlDbType.Date);

                    cmd.Parameters["@tungay"].Value = DateTime.ParseExact(tungay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    cmd.Parameters["@denngay"].Value = DateTime.ParseExact(denngay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    cmd.ExecuteNonQuery();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(_ptpc.pt);
                    }

                    cmd = new SqlCommand("sp_layPhieuChi", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tungay", SqlDbType.Date);
                    cmd.Parameters.Add("@denngay", SqlDbType.Date);

                    cmd.Parameters["@tungay"].Value = DateTime.Today;
                    cmd.Parameters["@denngay"].Value = DateTime.Today;

                    cmd.ExecuteNonQuery();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(_ptpc.pc);
                    }
                }
            }

            ViewBag.tungay = tungay;
            ViewBag.denngay = denngay;

            TongThu = 0;
            foreach (DataRow dr in _ptpc.pt.Rows)
            {
                TongThu += dr.Field<int>("TienThu");
            }
            TongChi = 0;
            foreach (DataRow dr in _ptpc.pc.Rows)
            {
                TongChi += dr.Field<int>("TienChi");
            }

            ViewBag.TongThu = TongThu;
            ViewBag.TongChi = TongChi;
            ViewBag.LoiNhuan = TongThu - TongChi;
            ViewBag.LoiNgay = 0;

            return View(_ptpc);
        } 
    }
}
