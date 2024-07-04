using System;

namespace OMSv2.Service.Entity
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public Guid ClientID { get; set; }
        public string SessionToken { get; set; }
    }
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Register : Login
    {
    }
}
