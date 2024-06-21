﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSv2.Service.Entity
{
    public class SaleDetails
    {
        /// <summary>
        /// unique identififier of SaleDetails
        /// </summary>
        public int SaleDetailsID { get; set; }

        /// <summary>
        /// SaleID of SaleDetails
        /// </summary>
        public int SaleID { get; set; }

        /// <summary>
        /// ItemID of SaleDetails
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// Price of saledetails
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Quantity of items in saledetails
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// CreatedName of saledetails
        /// </summary>
        public string CreatedName { get; set; }
        public string SaleName { get; set; }
        public string ItemName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
