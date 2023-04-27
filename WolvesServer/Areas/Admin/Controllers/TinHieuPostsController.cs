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
    public class TinHieuPostsController : Controller
    {
        private DBContext db = new DBContext();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        // GET: Admin/TinHieuPosts
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.TinHieuPosts.ToList();
                model.Reverse();
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/TinHieuPosts/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TinHieuPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,TP,SL")] TinHieuPost tinHieuPost,
            HttpPostedFileBase image)
        {
            if (Session["TaiKhoan"] != null)
            {
                string date = DateTime.Now.ToString("yyyy-M-d");
                tinHieuPost.Date = DateTime.Parse(date);
                tinHieuPost.SL = 0;
                tinHieuPost.TP = 0;
                if (image != null && image.ContentLength > 0)
                {
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/NormalSignal/" + fileName);
                    image.SaveAs(urlImage);


                    tinHieuPost.Image = "http://139.99.116.68/Image/NormalSignal/" + fileName;
                }

                if (ModelState.IsValid)
                {
                    db.TinHieuPosts.Add(tinHieuPost);
                    db.SaveChanges();
                    tinHieuPost = db.TinHieuPosts.ToList().Last();
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"TinHieuPost/{date}/{tinHieuPost.Id}", tinHieuPost);
                    return RedirectToAction("Index");
                }

                return View(tinHieuPost);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/TinHieuPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TinHieuPost tinHieuPost = db.TinHieuPosts.Find(id);
                if (tinHieuPost == null)
                {
                    return HttpNotFound();
                }

                return View(tinHieuPost);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TinHieuPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,TP,SL")] TinHieuPost tinHieuPost, HttpPostedFileBase Image)
        {
            if (Session["TaiKhoan"] != null)
            {
                tinHieuPost.SL = 0;
                tinHieuPost.TP = 0;
                if (Image != null && Image.ContentLength > 0)
                {
                    var byteImage = new byte[Image.ContentLength];
                    Image.InputStream.Read(byteImage, 0, Image.ContentLength);
                    string fileName = System.IO.Path.GetFileName(Image.FileName);
                    string urlImage = Server.MapPath("~/Image/" + fileName);
                    Image.SaveAs(urlImage);
                    tinHieuPost.Image = "http://139.99.116.68/Image/NormalSignal/" + fileName;
                }

                if (ModelState.IsValid)
                {
                    string date = DateTime.Now.ToString("yyyy-M-d");
                    tinHieuPost.Date = DateTime.Parse(date);
                    IFirebaseClient client = new FireSharp.FirebaseClient(config);
                    client.Set($"TinHieuPost/{date}/{tinHieuPost.Id}", tinHieuPost);
                    db.Entry(tinHieuPost).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tinHieuPost);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // GET: Admin/TinHieuPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TinHieuPost tinHieuPost = db.TinHieuPosts.Find(id);
                if (tinHieuPost == null)
                {
                    return HttpNotFound();
                }

                return View(tinHieuPost);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TinHieuPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                TinHieuPost tinHieuPost = db.TinHieuPosts.Find(id);
                db.TinHieuPosts.Remove(tinHieuPost);
                db.SaveChanges();
                IFirebaseClient client = new FireSharp.FirebaseClient(config);
                if (tinHieuPost.Date != null)
                    client.Delete($"TinHieuPost/{tinHieuPost.Date.Value:yyyy-M-d}/{tinHieuPost.Id}");
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