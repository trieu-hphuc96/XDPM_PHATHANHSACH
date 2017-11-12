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
    public class NXBsController : Controller
    {
        private QLPhatHanhSachDBContext db = new QLPhatHanhSachDBContext();

        // GET: NXBs
        public ActionResult Index()
        {
            return View(db.NXBs.ToList());
        }

        // GET: NXBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // GET: NXBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NXBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANXB,TENNXB,DIACHI,SDT,SOTK,TIENCONNO")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.NXBs.Add(nXB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nXB);
        }

        // GET: NXBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: NXBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANXB,TENNXB,DIACHI,SDT,SOTK,TIENCONNO")] NXB nXB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nXB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nXB);
        }

        // GET: NXBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: NXBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NXB nXB = db.NXBs.Find(id);
            db.NXBs.Remove(nXB);
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

        public ActionResult thongkeCongNoNXB()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_thongkeCongNoNXB", con);
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
        public ActionResult thongkeCongNoNXB(string ngay)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = DBConnection.connectDB())
            {
                SqlCommand cmd = new SqlCommand("sp_thongkeCongNoNXB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ngay", SqlDbType.Date);

                cmd.Parameters["@ngay"].Value = DateTime.ParseExact(ngay,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);

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
