using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WealthManager.Core.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}
