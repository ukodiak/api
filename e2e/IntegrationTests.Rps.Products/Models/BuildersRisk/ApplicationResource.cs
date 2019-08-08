using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class ApplicationResource : Models.ApplicationResource
    {
        [DataMember(Name = "isReOpened")]
        public bool IsReOpened { get; set; }

        [DataMember(Name = "creationDate")]
        public DateTime? CreationDate { get; set; }

        [DataMember(Name = "submissionDate")]
        public DateTime? SubmissionDate { get; set; }

        [DataMember(Name = "approvalDate")]
        public DateTime? ApprovalDate { get; set; }

        [DataMember(Name = "applicant")]
        public ApplicantData Applicant { get; set; }

        [DataMember(Name = "project")]
        public ProjectData Project { get; set; }

        [DataMember(Name = "coverage")]
        public CoverageData Coverage { get; set; }

        [DataMember(Name = "supplementalInfo")]
        public SupplementalData SupplementalInfo { get; set; }

        [DataMember(Name = "attachments")]
        public Dictionary<string, AttachmentResource> Attachments { get; set; }

        [DataMember(Name = "productName")]
        public string ProductName { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "hasPossibleOfacMatch")]
        public bool HasPossibleOfacMatch { get; set; }
    }
}
