using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WealthManager.Core.Model
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string CustodianId { get; set; }
        [Required]
        public string CustodianName { get; set; }
        [Required]
        public string RegisteredName { get; set; }
        public string ClientID { get; set; }
        [Required]
        public string CustodianAccountNumber { get; set; }
        [Required]
        public string MarketValue { get; set; }
        [Required]
        public string ProgramId { get; set; }
        [Required]
        public string ProgramName { get; set; }
        public bool IsClosed { get; set; }
        public string Token { get; set; }
    }
}
