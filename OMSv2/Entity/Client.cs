using System;

namespace OMSv2.Service.Entity
{
    public class Client
    {
        public Guid ClientID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string ApiKey { get; set; }
    }
}
