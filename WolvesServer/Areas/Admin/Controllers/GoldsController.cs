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
    public class GoldsController : Controller
    {
        private DBContext db = new DBContext();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        // GET: Admin/Golds
        public ActionResult Index()
        {
            var model = db.Golds.ToList();
            model.Reverse();
            return View(model);
        }


        // GET: Admin/Golds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Golds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Symbol,SoldOut,BuyInto,Date,Content")]
            Gold gold)
        {
            string date = DateTime.Now.ToString("yyyy-M-d");
            gold.Date = DateTime.Parse(date);
            gold.BuyInto = 0;
            gold.SoldOut = 0;

            if (ModelState.IsValid)
            {
                
                db.Golds.Add(gold);
                db.SaveChanges();
                gold = db.Golds.ToList().Last();
                IFirebaseClient client = new FireSharp.FirebaseClient(config);
                client.Set($"Golds/{date}/{gold.Id}", gold);
                return RedirectToAction("Index");
            }

            return View(gold);
        }

        // GET: Admin/Golds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gold gold = db.Golds.Find(id);

            if (gold == null)
            {
                return HttpNotFound();
            }

            return View(gold);
        }

        // POST: Admin/Golds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Symbol,SoldOut,BuyInto,Date,Content")]
            Gold gold)
        {
            if (ModelState.IsValid)
            {
                gold.BuyInto = 0;
                gold.SoldOut = 0;
                string date = DateTime.Now.ToString("yyyy-M-d");
                gold.Date = DateTime.Parse(date);
                IFirebaseClient client = new FireSharp.FirebaseClient(config);
                client.Set($"Golds/{date}/{gold.Id}", gold);
                db.Entry(gold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gold);
        }

        // GET: Admin/Golds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gold gold = db.Golds.Find(id);
            if (gold == null)
            {
                return HttpNotFound();
            }

            return View(gold);
        }

        // POST: Admin/Golds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gold gold = db.Golds.Find(id);
            db.Golds.Remove(gold);
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            if (gold.Date != null) client.Delete($"Golds/{gold.Date.Value:yyyy-M-d}/{gold.Id}");
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