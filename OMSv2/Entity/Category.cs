using System;

namespace OMSv2.Service.Entity
{
    public class Category
    {
        /// <summary>
        /// unique identififier of Category
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Name of Category
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// CreatedName of Category
        /// </summary>
        public string CreatedName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
