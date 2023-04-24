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
    public class ContactsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/Contacts
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                Contact contact = db.Contacts.FirstOrDefault();
                if (contact == null)
                {
                    return HttpNotFound();
                }
                return View(contact);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }



        // GET: Admin/Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Contact contact = db.Contacts.Find(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                return View(contact);
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        // POST: Admin/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Gmail,PhoneNumber,STK,NameBank,Bank,Website,Telegram")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
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
