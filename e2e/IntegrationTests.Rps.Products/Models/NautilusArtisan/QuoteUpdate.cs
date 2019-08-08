using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class QuoteUpdate
    {
        [DataMember(Name = "subtotal")]
        public decimal Subtotal { get; set; }

        [DataMember(Name = "terms")]
        public List<ArticleUpdate> Terms { get; set; }
    }

    [DataContract]
    public class ArticleUpdate
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "exclude")]
        public bool Exclude { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "rate")]
        public decimal Rate { get; set; }

        [DataMember(Name = "premium")]
        public decimal Premium { get; set; }
    }
}
