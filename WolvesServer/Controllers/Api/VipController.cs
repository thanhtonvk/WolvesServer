using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class VipController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpPost]
        [Route("vip/register-vip")]
        public IHttpActionResult RegisterVip(int month, int type, int wol, int idAccount)
        {
            int result = db.RegisterVIP(month, type, wol, idAccount);

            if (result > 0)
            {
                var model = db.VIPs.Where(x => x.IdAccount == idAccount).ToList();
                model.Reverse();
                var obj = model[0];
                VIP vip = new VIP()
                {
                    Account = null,
                    Start = obj.Start,
                    End = obj.End
                };
                return Ok(vip);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("vip/get-vip")]
        public IHttpActionResult CheckVip(int idAccount)
        {
            var result = db.CheckVIP(idAccount);
            return Ok(result);
        }

        [HttpGet]
        [Route("vip/get-list-vip")]
        public IHttpActionResult GetListVIP()
        {
            return Ok(db.PackVIPs.ToList());
        }
    }
}