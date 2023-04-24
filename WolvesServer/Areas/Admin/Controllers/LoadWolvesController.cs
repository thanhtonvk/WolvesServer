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
    public class LoadWolvesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/LoadWolves
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                var listWaiting = db.LoadWolves.Where(x => x.Status == 0).ToList();
                listWaiting.Reverse();
                var listConfirm = db.LoadWolves.Where(x => x.Status == 1).ToList();
                listConfirm.Reverse();
                var listCancel = db.LoadWolves.Where(x => x.Status == 2).ToList();
                listCancel.Reverse();
                ViewBag.ListWaiting = listWaiting;
                ViewBag.ListConfirm = listConfirm;
                ViewBag.ListCancel = listCancel;
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        public ActionResult Confirm(int id, int idAccount, int wolves)
        {
            if (Session["TaiKhoan"] != null)
            {
                var result = db.ConfirmWolves(id, idAccount, wolves);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
        public ActionResult Cancel(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.LoadWolves.Find(id);
                model.Status = 2;
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
