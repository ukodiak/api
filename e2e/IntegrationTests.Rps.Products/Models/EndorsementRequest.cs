using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    [DataContract]
    public class EndorsementRequest
    {
        [DataMember(Name = "requestText")]
        public string RequestText { get; set; }
    }
}
