using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class BusinessData
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "entityType")]
        public int EntityType { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "yearFounded")]
        public int YearFounded { get; set; }

        [DataMember(Name = "squareFootage")]
        public int SquareFootage { get; set; }

        [DataMember(Name = "glExposureAmount")]
        public int? GlExposureAmount { get; set; }

        [DataMember(Name = "physicalAddress")]
        public AddressData PhysicalAddress { get; set; }

        [DataMember(Name = "mailingAddress")]
        public AddressData MailingAddress { get; set; }

    }
}
