using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class ThongKeController : ApiController
    {
        [HttpGet]
        [Route("thongke/get-thong-ke")]
        public IHttpActionResult GetThongKe()
        {
            var model = new DBContext().TongQuats.ToList();
            model.Reverse();
            model.Take(1);
            return Ok(model);
        }
    }
}