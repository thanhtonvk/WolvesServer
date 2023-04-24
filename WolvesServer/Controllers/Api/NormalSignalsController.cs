using System;
using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class NormalSignalsController : ApiController
    {
        private DBContext _dbContext = new DBContext();

        [HttpGet]
        [Route("tinhieuthuong/get-tin-hieu-by-date")]
        public IHttpActionResult GetTinHieuByDate(DateTime dateTime)
        {
            var model = _dbContext.TinHieuPosts.Where(x => x.Date == dateTime).ToList();
            model.Reverse();
            return Ok(model);
        }

        [HttpGet]
        [Route("tinhieuthuong/get-last-tin-hieu")]
        public IHttpActionResult GetLastTinHieu(DateTime dateTime)
        {
            var model = _dbContext.TinHieuPosts.Where(x => x.Date == dateTime).ToList();
            model.Reverse();
            model.Take(1);
            return Ok(model);
        }
    }
}