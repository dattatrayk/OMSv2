using System;

namespace OMSv2.Service.Entity
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid ClientID { get; set; }
    }

}
