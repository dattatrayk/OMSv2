using System;

namespace OMSv2.Service.Entity
{
    public class Category:AuditInfo
    {
        /// <summary>
        /// unique identififier of Category
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Name of Category
        /// </summary>
        public string CategoryName { get; set; }

    }
}
