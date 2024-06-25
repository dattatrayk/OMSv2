using System;

namespace OMSv2.Service.Entity
{
    public class AuditInfo
    {
        public string CreatedName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid ClientID { get; set; }
    }
}
