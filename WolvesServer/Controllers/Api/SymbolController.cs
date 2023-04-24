using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class SymbolController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        [Route("Symbol/get-symbol")]
        public IHttpActionResult GetSymbol()
        {
            return Ok(db.GetSymbolList());
        }
        [HttpGet]
        [Route("Symbol/get-current-and-prev-symbol")]
        public IHttpActionResult GetCurrentAndPrev()
        {

            return Ok(db.GetCurrentAndPrevRate());
        }
    }
}