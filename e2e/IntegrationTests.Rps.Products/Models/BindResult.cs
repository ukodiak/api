﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    public class BindResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string PolicyNumber { get; set; }
    }
}
