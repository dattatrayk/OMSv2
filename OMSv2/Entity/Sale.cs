using System;
using System.Collections.Generic;

namespace OMSv2.Service.Entity
{
    public class Sale : AuditInfo
    {
        public Sale()
        {
            SaleDetail = new List<SaleDetails>();
        }
        /// <summary>
        /// unique identififier of Sale
        /// </summary>
        public int SaleID { get; set; }

        /// <summary>
        /// record the Date of the Sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Name of Customer
        /// </summary>
        public string CustomerName { get; set; }

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
        public string Address { get; set; }

        /// <summary>
        /// TotalAmount of sale
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// Quantity of items in  Sale
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Sales Details
        /// </summary>
        public List<SaleDetails> SaleDetail { get; set; }

    }

    public class SaleFilterParameter
    {
        public Guid ClientID { get; set; }
    }
}
