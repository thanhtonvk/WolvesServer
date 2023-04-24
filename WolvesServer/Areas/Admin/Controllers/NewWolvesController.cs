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
    public class NewWolvesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/NewWolves
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.NewWolves.ToList();
                model.Reverse();
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }


        // GET: Admin/NewWolves/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/NewWolves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titile,Content")] NewWolf newWolf, HttpPostedFileBase image)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/NewsWolves/" + fileName);
                    image.SaveAs(urlImage);


                    newWolf.Image = "http://103.29.0.41/Image/NewsWolves/" + fileName;



                }
                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    newWolf.Date = DateTime.Parse(date);
                
                    db.NewWolves.Add(newWolf);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(newWolf);
            }
            return RedirectToAction("Index", "Home", new { area = "" });


        }

        // GET: Admin/NewWolves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NewWolf newWolf = db.NewWolves.Find(id);
                if (newWolf == null)
                {
                    return HttpNotFound();
                }
                return View(newWolf);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/NewWolves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titile,Content")] NewWolf newWolf, HttpPostedFileBase image)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/NewsWolves/" + fileName);
                    image.SaveAs(urlImage);


                    newWolf.Image = "http://103.29.0.41/Image/NewsWolves/" + fileName;


                }
                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    newWolf.Date = DateTime.Parse(date);

                    db.Entry(newWolf).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(newWolf);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // GET: Admin/NewWolves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NewWolf newWolf = db.NewWolves.Find(id);
                if (newWolf == null)
                {
                    return HttpNotFound();
                }
                return View(newWolf);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/NewWolves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                NewWolf newWolf = db.NewWolves.Find(id);
                db.NewWolves.Remove(newWolf);
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
