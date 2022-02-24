using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WealthManager.Core.Model
{
    public class UserDisplayViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserId { get; set; }
        [Required]
        public int ClientType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Role")]
        public string RoleType { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required]
        [Remote("ValidatePhone", "Validation")]
        public string PhoneNo { get; set; }

        [Display(Name = "IsActive")]
        public bool HasActiveRole { get; set; }
    }
}
