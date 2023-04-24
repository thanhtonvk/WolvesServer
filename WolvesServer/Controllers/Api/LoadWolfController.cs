using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class LoadWolfController : ApiController
    {
        private DBContext db = new DBContext();
        [HttpPost]
        [Route("load-wolf/loading-wolf")]
        public IHttpActionResult LoadingWolf(int wolf, int idAccount)
        {
            int result = db.LoadingWolves(wolf, idAccount);

            if (result > 0) return Ok();
            return BadRequest();
        }

    }
}