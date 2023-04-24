
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
    public class AccountsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/Accounts
        public ActionResult Index(string keyword)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    return View(db.Accounts.ToList());
                }
                keyword = keyword.Trim().ToLower();
                return View(db.Accounts.Where(x => x.Email.ToLower().Contains(keyword) || x.FirstName.ToLower().Contains(keyword) || x.LastName.ToLower().Contains(keyword)));
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
        public ActionResult Block(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.Accounts.Find(id);
                model.IsActive = false;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });


        }
        public ActionResult Open(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.Accounts.Find(id);
                model.IsActive = true;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });



        }
        public ActionResult SetCTV(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.Accounts.Find(id);
                model.Type = 1;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
        public ActionResult TurnOffCTV(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
              
                var model = db.Accounts.Find(id);
                model.Type = 0;
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
