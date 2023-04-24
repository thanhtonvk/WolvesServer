using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class VideosController : ApiController
    {
        DBContext dbContext = new DBContext();

        [HttpGet]
        [Route("videos/get")]
        public IHttpActionResult GetVideo()
        {
            var model = dbContext.Videos.ToList();
            model.Reverse();
            var rs = model.Take(10);
            return Ok(rs);
        }
    }
}