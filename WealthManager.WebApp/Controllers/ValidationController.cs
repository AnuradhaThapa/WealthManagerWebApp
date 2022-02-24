using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WealthManager.WebApp.Controllers
{
    public class ValidationController : Controller
    {
        public IActionResult ValidatePhone(string phoneNo)
      {
            if (!Regex.IsMatch(phoneNo, @"^\d+$"))
                return Json(data: "Invalid phone number.");
            if(phoneNo.Length < 10)
                return Json(data: "Enter phone number of 10 digits.");
            return Json(data: true);
        }
    }
}
