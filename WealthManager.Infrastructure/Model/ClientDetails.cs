using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Infrastructure.Model
{
    public class ClientDetails
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int ClientType { get; set; }
        public string AgentId { get; set; }
    }
}
