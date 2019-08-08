using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class SpecialtyResource
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "exposure")]
        public ExposureType Exposure { get; set; }
    }

    public enum ExposureType
    {
        None = -1,
        GrossSales,
        Payroll,
        Area
    }
}
