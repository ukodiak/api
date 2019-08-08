using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class QuoteUpdate
    {
        [DataMember(Name = "subtotal")]
        public decimal Subtotal { get; set; }

        [DataMember(Name = "terms")]
        public ArticleUpdate[] Terms { get; set; }
    }

    public class ArticleUpdate
    {
        #region Public Properties
        [DataMember(Name = "exclude")]
        public bool Exclude { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        #endregion
    }
}
