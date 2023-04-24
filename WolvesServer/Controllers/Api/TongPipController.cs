using System;
using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class TongPipController : ApiController
    {
        [HttpGet]
        [Route("tongpip/get")]
        public IHttpActionResult GetTongPip(DateTime dateTime)
        {
            var model = new DBContext().ThongKes.Where(x => x.Date == dateTime).ToList();
            model.Reverse();
            return Ok(model);
        }
    }
}