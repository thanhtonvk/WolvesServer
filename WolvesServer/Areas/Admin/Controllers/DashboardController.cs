using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using WolvesServer.EF;

namespace WolvesServer.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private DBContext db = new DBContext();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.ThongKes.ToList();
                model.Reverse();
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }


        // GET: Admin/Dashboard/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/Dashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Money,Date,PipCu,PipMoi,SL,TP")]
            ThongKe thongKe)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    thongKe.Date = DateTime.Parse(date);
                    db.ThongKes.Add(thongKe);
                    db.SaveChanges();
                    thongKe = db.ThongKes.ToList().Last();
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"TongPIP/{date}/{thongKe.Id}", thongKe);
                    return RedirectToAction("Index");
                }

                return View(thongKe);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/Dashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ThongKe thongKe = db.ThongKes.Find(id);
                if (thongKe == null)
                {
                    return HttpNotFound();
                }

                return View(thongKe);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/Dashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Money,PipCu,PipMoi,SL,TP")]
            ThongKe thongKe)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    thongKe.Date = DateTime.Parse(date);
                    db.Entry(thongKe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(thongKe);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/Dashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ThongKe thongKe = db.ThongKes.Find(id);
                if (thongKe == null)
                {
                    return HttpNotFound();
                }

                return View(thongKe);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                ThongKe thongKe = db.ThongKes.Find(id);
                db.ThongKes.Remove(thongKe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home", new {area = ""});
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