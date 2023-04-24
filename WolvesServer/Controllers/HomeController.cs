using WolvesServer.EF;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WolvesServer.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string email, string password)
        {
            try
            {
                User user = null;
                FirebaseAuthProvider authProvider =
                    new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBaz6R-fqMhsSytluHgncdaSUDytgflLQM"));
                await authProvider.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        user = task.Result.User;
                    }
                });
                if (user != null)
                {
                    var model = db.LoginAccount(user.Email).FirstOrDefault();
                    if (model != null)
                    {
                        Session["TaiKhoan"] = model;
                        if (model.Type == 2)
                        {
                            return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
                        }
                        else
                        {
                            return RedirectToAction("Index", "NapWol");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác";
                        return View();
                    }
                }

                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Lỗi đăng nhập";

                return View();
            }
        }
    }
}