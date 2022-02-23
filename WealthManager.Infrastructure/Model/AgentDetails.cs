using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Infrastructure.Model
{
    public class AgentDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string RoleType { get; set; }
        public bool HasActiveRole { get; set; }
    }
}
