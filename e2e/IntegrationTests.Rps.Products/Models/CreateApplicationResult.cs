using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    public class CreateApplicationResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public int ApplicationId { get; set; }
    }
}
