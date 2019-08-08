using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    [DataContract]
    public class ApplicationResource
    {
        #region Public Properties

        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "producerId")]
        public int? ProducerId { get; set; }

        [DataMember(Name = "producerOfficeId")]
        public int? ProducerOfficeId { get; set; }

        [DataMember(Name = "productId")]
        public Guid ProductId { get; set; }

        #endregion
    }
}
