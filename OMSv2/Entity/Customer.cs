using System;

namespace OMSv2.Service.Entity
{
    public class Customer : AuditInfo
    {
        public Customer()
        {
            AddressDetails = new AddressDetails();
        }
        /// <summary>
        /// unique identififier of Sale
        /// </summary>
        public int CustomerID { get; set; }


        /// <summary>
        /// Name of Customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ContactNo of Customer
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// Email of Customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Address of Customer
        /// </summary>
        public AddressDetails AddressDetails { get; set; }

    }
    public class AddressDetails
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
    }
    public class CustomerFilterParameter
    {
        public Guid ClientID { get; set; }
    }
}
