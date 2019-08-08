using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models
{
    public class AddressData
    {
        #region Public Properties

        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }

        /// <summary>
        /// County
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string Locality { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string Region { get; set; }

        #endregion

        #region Public Methods

        public string GetSingleLineAddress()
        {
            var addressLine = string.IsNullOrEmpty(this.AddressLineTwo) ? this.AddressLineOne : $"{this.AddressLineOne}, {this.AddressLineTwo}";

            var addressString = $"{addressLine}, {this.Locality}, {this.Region}";

            return addressString;
        }

        public AddressData Merge(AddressData newData)
        {
            if (newData == null)
            {
                return this;
            }

            if (!string.IsNullOrEmpty(newData.AddressLineOne))
            {
                this.AddressLineOne = newData.AddressLineOne;
            }

            if (!string.IsNullOrEmpty(newData.AddressLineTwo))
            {
                this.AddressLineTwo = newData.AddressLineTwo;
            }

            if (!string.IsNullOrEmpty(newData.District))
            {
                this.District = newData.District;
            }

            if (!string.IsNullOrEmpty(newData.Locality))
            {
                this.Locality = newData.Locality;
            }

            if (!string.IsNullOrEmpty(newData.PostalCode))
            {
                this.PostalCode = newData.PostalCode;
            }

            if (!string.IsNullOrEmpty(newData.Region))
            {
                this.Region = newData.Region;
            }

            return this;
        }

        #endregion
    }
}
