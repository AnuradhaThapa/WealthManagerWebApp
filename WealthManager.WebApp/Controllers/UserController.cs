using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WealthManager.Core.Common;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.Interface;
using WealthManager.WebApp.Authorize;

namespace WealthManager.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository iuserRepository;
        /// <summary>
        /// UsersController Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            iuserRepository = userRepository;//
        }

        [Authorize("Admin,Agent")]
        [HttpGet]
        public async Task<IActionResult> UserDetails(string userName)
        {
            UserDisplayViewModel userDisplayViewModel = null;
            if (!string.IsNullOrEmpty(userName))
            {
                
                userDisplayViewModel = await iuserRepository.GetUserDetailsByName(userName);
            }
            return View(userDisplayViewModel);
        }

        [Authorize("Admin,Agent")]
        public async Task<IActionResult> EditUser(string userName)
        {
            UserDisplayViewModel userDisplayViewModel = new UserDisplayViewModel();
            if (!string.IsNullOrEmpty(userName))
            {
                
                userDisplayViewModel = await iuserRepository.GetUserDetailsByName(userName);
            }
            return View(userDisplayViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDisplayViewModel userDisplayViewModel)
        {
            if (ModelState.IsValid)
            {
                string response = string.Empty;
                if (userDisplayViewModel != null)
                {
                    
                    response = await iuserRepository.UpdateUser(userDisplayViewModel);
                    TempData["DataSavedMessage"] = response;
                    ModelState.Clear();
                }
            }
            return RedirectToAction("UserDetails", new { userName = userDisplayViewModel.UserName });
        }

        [Authorize("Admin")]
        public async Task<IActionResult> AgentsDetails()
        {
            
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            userViewModels = await iuserRepository.GetAgents();
            if(userViewModels != null)
            return View(userViewModels);
            else
            {
                TempData["DataSavedMessage"] = "No agent found..";
                return RedirectToAction("UserDetails", new { userName = User.Identity.Name });
            }

        }

        [Authorize("Agent")]
        public async Task<IActionResult> ClientsDetailsByAgent(string userId)
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            
            userViewModels = await iuserRepository.GetClientsByAgent(userId);
            if (userViewModels != null)
            {
                return View(userViewModels);
            }
            else
            {
                TempData["DataSavedMessage"] = "No client found..";
                return RedirectToAction("UserDetails", new { userName = User.Identity.Name });
            }
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult AddAgent()
        {
            return View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAgent(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                string response = string.Empty;
                if (userViewModel != null)
                {
                    userViewModel.RoleType = "Agent";
                    userViewModel.UserId =  "AG" + CommonMethods.GenerateRandomNo().ToString();
                    string responseAuth = await iuserRepository.CreateUserAuth(userViewModel);
                    if (responseAuth == "Created")
                    {                      
                        response = await iuserRepository.CreateAgent(userViewModel);
                        TempData["DataSavedMessage"] = response;
                        ModelState.Clear();
                    }
                    else
                        TempData["DataSavedMessage"] = responseAuth;
                }
            }
            return View();
        }

        [Authorize("Agent")]
        public IActionResult AddClient()
        {
            return View();
        }

        [Authorize("Agent")]
        [HttpPost]
        public async Task<IActionResult> AddClient(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                string response = string.Empty;
                if (userViewModel != null)
                {
                    userViewModel.RoleType = "Client";
                    userViewModel.UserId = "CL" + CommonMethods.GenerateRandomNo().ToString();
                    string responseAuth = await iuserRepository.CreateUserAuth(userViewModel);
                    if (responseAuth == "Created")
                    {                 
                        response = await iuserRepository.CreateClient(userViewModel);
                        TempData["DataSavedMessage"] = response;
                        ModelState.Clear();
                    }
                    else
                        TempData["DataSavedMessage"] = responseAuth;
                }
            }
            return View();
        }
    }
}
