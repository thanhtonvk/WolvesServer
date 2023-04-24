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
    public class DoiTacsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/DoiTacs
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(db.DoiTacs.ToList());
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // GET: Admin/DoiTacs/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/DoiTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenDoiTac,TrangWeb,ThongTinKhac")] DoiTac doiTac)
        {
            if (Session["TaiKhoan"] != null)
            {
               
                if (ModelState.IsValid)
                {
                    db.DoiTacs.Add(doiTac);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(doiTac);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // GET: Admin/DoiTacs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DoiTac doiTac = db.DoiTacs.Find(id);
                if (doiTac == null)
                {
                    return HttpNotFound();
                }
                return View(doiTac);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/DoiTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenDoiTac,TrangWeb,ThongTinKhac")] DoiTac doiTac)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(doiTac).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(doiTac);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // GET: Admin/DoiTacs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DoiTac doiTac = db.DoiTacs.Find(id);
                if (doiTac == null)
                {
                    return HttpNotFound();
                }
                return View(doiTac);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/DoiTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                DoiTac doiTac = db.DoiTacs.Find(id);
                db.DoiTacs.Remove(doiTac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });

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
