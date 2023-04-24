using System.Linq;
using System.Web.Mvc;
using WolvesServer.EF;

namespace WolvesServer.Controllers
{
    public class DangKyVIPController : Controller
    {
        private DBContext _db = new DBContext();

        // GET
        public ActionResult Index()
        {
            var model = _db.PackVIPs.ToList();
            return View(model);
        }

        public ActionResult RegisterVip(int id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var packVip = _db.PackVIPs.FirstOrDefault(x => x.Id == id);
                var account = (LoginAccount_Result) Session["TaiKhoan"];
                if (account.Wolves >= packVip.Wol)
                {
                    var result = _db.RegisterVIP(packVip.Month, 1, packVip.Wol, account.Id);
                    var model = _db.CheckVIP(account.Id);
                    return View(model);
                }

                ViewBag.ThongBao = "Số dư không đủ, vui lòng nạp thêm";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}