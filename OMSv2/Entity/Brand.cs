using System;

namespace OMSv2.Service.Entity
{
    public class Brand : AuditInfo
    {
        /// <summary>
        /// unique identififier of Brand
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Name of Brand
        /// </summary>
        public string BrandName { get; set; }

    }
}
