using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class InviteController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpPost]
        [Route("invite/input-invite")]
        public IHttpActionResult Invite(int presenter, int presentee)
        {
            int result = db.InputInvite(presenter, presentee);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("invite/get-invited")]
        public IHttpActionResult GetInvited(int id)
        {
            var invited = db.GetInvited(id);
            if (invited != null) return Ok(invited);
            return BadRequest();
        }
        

    }
}