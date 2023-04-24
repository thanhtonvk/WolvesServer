using System.Linq;
using System.Web.Mvc;
using WolvesServer.EF;
using WolvesServer.Models;

namespace WolvesServer.Controllers
{
    public class NapWolController : Controller
    {
        private DBContext _db = new DBContext();

        private Wol[] _wols = new[]
        {
            new Wol() {Id = 0, Money = 240000, Quantity = 10},
            new Wol() {Id = 1, Money = 1200000, Quantity = 50},
            new Wol() {Id = 2, Money = 2400000, Quantity = 100},
            new Wol() {Id = 3, Money = 4800000, Quantity = 200},
            new Wol() {Id = 4, Money = 12000000, Quantity = 500},
            new Wol() {Id = 5, Money = 24000000, Quantity = 1000},
            new Wol() {Id = 6, Money = 36000000, Quantity = 1500},
            new Wol() {Id = 7, Money = 48000000, Quantity = 2000},
            new Wol() {Id = 8, Money = 72000000, Quantity = 3000},
        };

        // GET
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View(_wols);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoadWol(int idWols)
        {
            if (Session["TaiKhoan"]!=null)
            {
              
                var wol = _wols.FirstOrDefault(x => x.Id == idWols);
              var account = (LoginAccount_Result) Session["TaiKhoan"];
                var result = _db.LoadingWolves(wol.Quantity, account.Id);
                return View(wol);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}