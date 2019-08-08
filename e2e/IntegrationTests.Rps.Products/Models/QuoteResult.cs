using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    public class QuoteResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Quote { get; set; }
    }
}
