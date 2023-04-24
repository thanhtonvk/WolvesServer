using System;
using System.Linq;
using System.Web.Http;
using WolvesServer.EF;

namespace WolvesServer.Controllers.Api
{
    public class NewsController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        [Route("news/get-all-news")]
        public IHttpActionResult GetNews()
        {
            var model = db.News.Where(x => x.Type == false).ToList();
            model.Reverse();
            return Ok(model);
        }

        [HttpGet]
        [Route("news/get-all-news-vip")]
        public IHttpActionResult GetNewsVip()
        {
            var model = db.News.Where(x => x.Type == true).ToList();
            model.Reverse();
            return Ok(model);
        }

        [HttpGet]
        [Route("news/get-news-by-date")]
        public IHttpActionResult GetNewByDate(DateTime dateTime)
        {
            var model = db.News.Where(x => x.Type == false&&x.Date==dateTime).ToList();
            model.Reverse();
            return Ok(model);
        }

        [HttpGet]
        [Route("news/get-news-vip-by-date")]
        public IHttpActionResult GetNewsVipByDate(DateTime dateTime)
        {
            var model = db.News.Where(x => x.Type == true&&x.Date==dateTime).ToList();
            model.Reverse();
            return Ok(model);
        }

        [HttpGet]
        [Route("news/ban-lenh")]
        public IHttpActionResult GetBanLenh(DateTime dateTime)
        {
            return Ok(db.GetBanLenh(dateTime).Reverse());
        }
        [HttpGet]
        [Route("news/get-last-ban-lenh")]
        public IHttpActionResult GetLast()
        {

            return Ok(db.GetBanLenh(DateTime.Now).Reverse().Take(1));
        }

        [HttpGet]
        [Route("new/contact")]
        public IHttpActionResult GetContact()
        {
            return Ok(db.Contacts.ToList());
        }
    }
}