using System;
using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class SanGiaoDichsController : ApiController
    {
        private DBContext _dbContext = new DBContext();
        [HttpGet]
        [Route("sangiaodich/get")]
        public IHttpActionResult GetGiaoDichByDate()
        {
            var model = _dbContext.SanGiaoDiches.ToList();
            model.Reverse();
            return Ok(model);
        }
    }
}