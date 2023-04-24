using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class DoiTacController:ApiController
    {
        public IHttpActionResult GetDoiTac()
        {
            var model = new DBContext().DoiTacs.ToList();
            return Ok(model);
        }
    }
}