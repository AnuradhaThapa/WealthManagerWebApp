using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.APIResponse;
using WealthManager.WebApp.API.Interface;

namespace WealthManager.WebApp.Controllers
{
    [AllowAnonymous]
    public class LoginController:Controller 
    {
        ILoginRepository iloginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            iloginRepository = loginRepository;
        }

        [HttpGet]
        public  IActionResult SignIn()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = null;
                if (loginViewModel != null)
                {
                    var token = await iloginRepository.ValidateUserAuth(loginViewModel);
                    if (token != null)
                    {
                        HttpContext.Session.SetString("Token", token.auth_token);
                        new ApiResponse(this.HttpContext);
                        userViewModel = await iloginRepository.ValidateUser(loginViewModel.Username);
                    }                  
                    if (userViewModel != null)
                    {
                        if (userViewModel.RoleType == "Admin" || userViewModel.RoleType == "Agent")
                            return RedirectToAction("UserDetails", "User",new { userName = loginViewModel.Username });
                        if (userViewModel.RoleType == "Client")
                            return RedirectToAction("ClientDetails", "Client", new { userName = loginViewModel.Username });
                    }
                }
                ModelState.Clear();
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View();
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SignOut()
        {
             HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}
