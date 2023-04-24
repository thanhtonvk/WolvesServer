using WolvesServer.EF;
using System;
using System.Linq;
using System.Web.Http;

namespace WolvesServer.Controllers.Api
{
    public class NewWolvesController : ApiController
    {
        [HttpGet]
        [Route("news-wolves/get")]
        public IHttpActionResult GetNewsWolves(DateTime dateTime)
        {
            DBContext db = new DBContext();
            var model = db.NewWolves.ToList();
            model.Reverse();
            return Ok(model);
        }
    }
}
