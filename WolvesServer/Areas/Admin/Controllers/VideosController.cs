using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WolvesServer.EF;

namespace WolvesServer.Areas.Admin.Controllers
{
    public class VideosController : Controller
    {
        // GET
        private DBContext db = new DBContext();

        // GET: Admin/TinHieuPosts
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.Videos.ToList();
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,LinkVideo,LinkYoutube")]
            Video video, HttpPostedFileBase image)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/Video/" + fileName);
                    image.SaveAs(urlImage);
                    video.LinkVideo = "http://103.29.0.41/Image/Video/" + fileName;
                }

                if (ModelState.IsValid)
                {
                    db.Videos.Add(video);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(video);
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

                Video video = db.Videos.Find(id);
                if (video == null)
                {
                    return HttpNotFound();
                }

                return View(video);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        // POST: Admin/TinHieuPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =  "Id,Content,LinkVideo,LinkYoutube")] Video video, HttpPostedFileBase image)
        {
            if (Session["TaiKhoan"] != null)
            {
                
                if (image != null && image.ContentLength > 0)
                {
                    var byteImage = new byte[image.ContentLength];
                    image.InputStream.Read(byteImage, 0, image.ContentLength);


                    string fileName = System.IO.Path.GetFileName(image.FileName);

                    string urlImage = Server.MapPath("~/Image/Video/" + fileName);
                    image.SaveAs(urlImage);
                    video.LinkVideo = "http://103.29.0.41/Image/Video/" + fileName;
                }


                if (ModelState.IsValid)
                {
                  
                    db.Entry(video).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(video);
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

                Video video = db.Videos.Find(id);
                if (video == null)
                {
                    return HttpNotFound();
                }

                return View(video);
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
                Video video = db.Videos.Find(id);
                db.Videos.Remove(video);
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