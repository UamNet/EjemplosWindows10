using RestApiAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiAsp.Controllers
{
    public class QuotesController : ApiController
    {
        static List<Quote> quotes = null;

        public QuotesController()
        {
            if (quotes == null)
            {
                quotes = new List<Quote>();
                quotes.Add(new Quote { Id = 1, Text = "No sabeis hacer makefiles" });
                quotes.Add(new Quote { Id = 2, Text = "Se masca la tragedia" });
            }
        }

        public IEnumerable<Quote> GetAllQuotes()
        {
            return quotes;
        }

        public IHttpActionResult GetQuote(int id)
        {
            var quote = quotes.FirstOrDefault((p) => p.Id == id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        [HttpPost]
        [Route("api/quotes/new/{quote}")]
        public void AddQuote(string quote)
        {
            var id = quotes.Select(x => x.Id).Max()+1;
            Quote newQuote = new Quote { Id = id, Text = quote };
            quotes.Add(newQuote);
        }
    }

}
