using WolvesServer.EF;

using System.Web.Http;

namespace WolvesServer.Controllers.Api
{
    public class DoiTienController : ApiController
    {
        [HttpPost]
        [Route("money/doi-tien")]
        public IHttpActionResult InsertMoney(DoiTien doiTien)
        {
            DBContext db = new DBContext();
            var result = db.InsertDoiTien(doiTien.IdAccount, doiTien.Quantity, doiTien.STK, doiTien.Bank, doiTien.NameBank);
            if (result > 0) return Ok();
            return BadRequest();
        }
    }
}
