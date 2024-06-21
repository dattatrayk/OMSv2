using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSv2.Service.Entity
{
    public class Category
    {
        /// <summary>
        /// unique identififier of Category
        /// </summary>
        public int CategoryID { get; set; }

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
