using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSv2.Service.Entity
{
    public class Brand
    {
        /// <summary>
        /// unique identififier of Brand
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Name of Brand
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// CreatedName of Brand
        /// </summary>
        public string CreatedName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
