using OMSv2.Service.Entity;
using System;

namespace OMSv2.Service
{
    public class Item : AuditInfo
    {
        /// <summary>
        /// unique identififier of Item
        /// </summary>
        public Guid ItemID { get; set; }

        /// <summary>
        /// Name of Item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of Item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price of item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// ImgURL of item
        /// </summary>
        public string ImgURL { get; set; }

        /// <summary>
        /// Stock of item
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// CategoryID of item
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// BrandID of item
        /// </summary>
        public Guid BrandID { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }
    }
    public class ItemFilterParameter
    {
        public Guid ClientID { get; set; }
        public Guid BrandID { get; set; }
        public Guid CategoryID { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public bool IsInStock { get; set; }
    }
}
