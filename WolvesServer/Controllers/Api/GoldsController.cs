using System;
using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class GoldsController : ApiController
    {
        private DBContext _dbContext = new DBContext();
        [HttpGet]
        [Route("golds/get-gold-by-date")]
        public IHttpActionResult GetGiaVangByDate(DateTime dateTime)
        {
            var model = _dbContext.Golds.Where(x => x.Date == dateTime).ToList();
            model.Reverse();
            return Ok(model);
        }
        [HttpGet]
        [Route("gold/get-last-gold")]
        public IHttpActionResult GetLastGiaVang()
        {
            
            var model = _dbContext.Golds.Where(x => x.Date == DateTime.UtcNow.Date).ToList();
            model.Reverse();
            model.Take(1);
            return Ok(model);
        }
    }
}