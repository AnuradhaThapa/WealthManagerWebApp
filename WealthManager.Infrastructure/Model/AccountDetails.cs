using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Infrastructure.Model
{
    public class AccountDetails
    {
        public string AccountId { get; set; }

        public string CustodianId { get; set; }

        public string CustodianName { get; set; }

        public string RegisteredName { get; set; }

        public string CustodianAccountNumber { get; set; }

        public string MarketValue { get; set; }

        public string ProgramId { get; set; }

        public string ProgramName { get; set; }

        public string ClientId { get; set; }

        public bool IsClosed { get; set; }
    }
}
