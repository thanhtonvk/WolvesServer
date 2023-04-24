using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WolvesServer.EF;

namespace WolvesServer.Areas.Admin.Controllers
{
    public class SanGiaoDichsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/SanGiaoDichs
        public ActionResult Index()
        {
            var model = db.SanGiaoDiches.ToList();
            model.Reverse();
            return View(model);
        }

     

        // GET: Admin/SanGiaoDichs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SanGiaoDichs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titile,Content")] SanGiaoDich sanGiaoDich)
        {
            if (ModelState.IsValid)
            {
                db.SanGiaoDiches.Add(sanGiaoDich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanGiaoDich);
        }

        // GET: Admin/SanGiaoDichs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanGiaoDich sanGiaoDich = db.SanGiaoDiches.Find(id);
            if (sanGiaoDich == null)
            {
                return HttpNotFound();
            }
            return View(sanGiaoDich);
        }

        // POST: Admin/SanGiaoDichs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titile,Content")] SanGiaoDich sanGiaoDich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanGiaoDich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanGiaoDich);
        }

        // GET: Admin/SanGiaoDichs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanGiaoDich sanGiaoDich = db.SanGiaoDiches.Find(id);
            if (sanGiaoDich == null)
            {
                return HttpNotFound();
            }
            return View(sanGiaoDich);
        }

        // POST: Admin/SanGiaoDichs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanGiaoDich sanGiaoDich = db.SanGiaoDiches.Find(id);
            db.SanGiaoDiches.Remove(sanGiaoDich);
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
