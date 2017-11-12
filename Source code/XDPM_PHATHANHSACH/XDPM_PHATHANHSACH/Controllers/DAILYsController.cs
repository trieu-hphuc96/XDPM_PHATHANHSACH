using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using XDPM_PHATHANHSACH.Models;

namespace XDPM_PHATHANHSACH.Controllers
{
    public class DAILYsController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: DAILYs
        public ActionResult Index()
        {
            return View(db.DAILies.ToList());
        }

        // GET: DAILYs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // GET: DAILYs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DAILYs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADL,TENDL,DIACHI,TONGSOSACH,TIENCONNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.DAILies.Add(dAILY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dAILY);
        }

        // GET: DAILYs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILYs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADL,TENDL,DIACHI,TONGSOSACH,TIENCONNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAILY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dAILY);
        }

        // GET: DAILYs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = db.DAILies.Find(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILYs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAILY dAILY = db.DAILies.Find(id);
            db.DAILies.Remove(dAILY);
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

        public ActionResult thongkeCongNoDaiLy()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_thongkeCongNoDaiLy", con);
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
        public ActionResult thongkeCongNoDaiLy(string ngay)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_thongkeCongNoDaiLy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ngay", SqlDbType.Date);

                cmd.Parameters["@ngay"].Value = DateTime.ParseExact(ngay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            ViewBag.ngay = ngay;

            return View(dt);
        }
    }
}
