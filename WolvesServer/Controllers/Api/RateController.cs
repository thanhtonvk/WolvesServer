using System;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class RateController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        [Route("rate/last-rate-by-name")]
        public IHttpActionResult GetLastRateByName(string name)
        {
            return Ok(db.GetLastRateByName(name));
        }

        [HttpGet]
        [Route("rate/get-rate-by-name-and-date")]
        public IHttpActionResult GetRateByNameAndDate(string name, DateTime dateTime)
        {
            return Ok(db.GetRateByNameAndDate(name, dateTime));
        }
        [HttpGet]
        [Route("rate/get-min-max")]
        public IHttpActionResult GetMinMax(string name, DateTime dateTime)
        {
            return Ok(db.GetMinMaxInDay(name, dateTime));
        }
        [HttpGet]
        [Route("rate/get-current-prev")]
        public IHttpActionResult GetCurrentPrev(string name, DateTime dateTime)
        {
            return Ok(db.GetCurrentAndPrevByNameAndDay(name, dateTime));
        }
    }
}