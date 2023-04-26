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
    public class NewWolvesController : Controller
    {
        private DBContext db = new DBContext();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        // GET: Admin/NewWolves
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.NewWolves.ToList();
                model.Reverse();
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }


        // GET: Admin/NewWolves/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new {area = ""});
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
                    newWolf.Image = "http://139.99.116.68/Image/NewsWolves/" + fileName;
                }

                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    newWolf.Date = DateTime.Parse(date);
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"NewWolves/{date}/{newWolf.Id}", newWolf);
                    db.NewWolves.Add(newWolf);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(newWolf);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
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

            return RedirectToAction("Index", "Home", new {area = ""});
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
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/NewsWolves/" + fileName);
                    image.SaveAs(urlImage);


                    newWolf.Image = "http://139.99.116.68/Image/NewsWolves/" + fileName;
                }

                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    newWolf.Date = DateTime.Parse(date);
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"NewWolves/{date}/{newWolf.Id}", newWolf);
                    db.Entry(newWolf).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(newWolf);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
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

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/NewWolves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                IFirebaseClient client = new FireSharp.FirebaseClient(config);
                NewWolf newWolf = db.NewWolves.Find(id);
                if (newWolf != null)
                {
                    if (newWolf.Date != null)
                        client.Delete($"NewWolves/{newWolf.Date.Value:yyyy-M-d}/{newWolf.Id}");
                    db.NewWolves.Remove(newWolf);
                }

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