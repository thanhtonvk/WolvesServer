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
    public class DoiTiensController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/DoiTiens
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
  var listWaiting = db.DoiTiens.Where(x => x.Status == false).ToList();
            listWaiting.Reverse();
            var listConfirm = db.DoiTiens.Where(x => x.Status == true).ToList();
            listConfirm.Reverse();
          
            ViewBag.ListWaiting = listWaiting;
            ViewBag.ListConfirm = listConfirm;
            
            return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });

          
        }
        public ActionResult Confirm(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
  var model = db.DoiTiens.Find(id);
            model.Status = true;
            db.Entry(model).State = EntityState.Modified;
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
