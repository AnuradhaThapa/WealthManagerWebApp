using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Infrastructure.Model
{
    public class UserDetailsEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int ClientType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleType { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string HasActiveRole { get; set; }
        public string AgentId { get; set; }
    }
}
