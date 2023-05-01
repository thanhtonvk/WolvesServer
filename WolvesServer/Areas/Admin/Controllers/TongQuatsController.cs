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
    public class TongQuatsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/TongQuats
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.TongQuats.ToList();
                model.Reverse();
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }


        // GET: Admin/TongQuats/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        // POST: Admin/TongQuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        private SendNotification _sendNotification = new SendNotification();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TongPip,Trades,WinRate")]
            TongQuat tongQuat)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.TongQuats.Add(tongQuat);
                    db.SaveChanges();
                    tongQuat = db.TongQuats.ToList().Last();
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"TongQuat/{tongQuat.Id}", tongQuat);
                    _sendNotification.Send(
                        $"Trades: {tongQuat.Trades}\nTổng pip: {tongQuat.TongPip}\nWinrates: {tongQuat.WinRate}");
                    return RedirectToAction("Index");
                }

                return View(tongQuat);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/TongQuats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TongQuat tongQuat = db.TongQuats.Find(id);
                if (tongQuat == null)
                {
                    return HttpNotFound();
                }

                return View(tongQuat);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TongQuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TongPip,Trades,WinRate")]
            TongQuat tongQuat)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tongQuat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tongQuat);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/TongQuats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TongQuat tongQuat = db.TongQuats.Find(id);
                if (tongQuat == null)
                {
                    return HttpNotFound();
                }

                return View(tongQuat);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TongQuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                TongQuat tongQuat = db.TongQuats.Find(id);
                db.TongQuats.Remove(tongQuat);
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