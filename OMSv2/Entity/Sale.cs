using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSv2.Service.Entity
{
    public class Sale
    {
        /// <summary>
        /// unique identififier of Sale
        /// </summary>
        public int SaleID { get; set; }

        /// <summary>
        /// record the Date of the Sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

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
        /// CreatedName of Sale
        /// </summary>
        public string CreatedName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
