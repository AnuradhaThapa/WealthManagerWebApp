using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.Interface;
using WealthManager.WebApp.Authorize;

namespace WealthManager.WebApp.Controllers
{
    [Authorize("Client")]
    public class ClientController : Controller
    {
        private readonly IClientRepository iclientRepository;
        /// <summary>
        /// ClientController Constructor
        /// </summary>
        /// <param name="clientRepository"></param>
        public ClientController(IClientRepository clientRepository)
        {
            iclientRepository = clientRepository;
        }
        public async Task<IActionResult> ClientDetails(string userName)
        {
            ClientViewModel clientViewModel = new ClientViewModel();
            if (!string.IsNullOrEmpty(userName))
            {            
                clientViewModel = await iclientRepository.GetClientDetailsByName(userName);
            }
            return View(clientViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditClient(string userName)
        {
            ClientViewModel clientViewModel = new ClientViewModel();
            if (!string.IsNullOrEmpty(userName))
            {
                
                clientViewModel = await iclientRepository.GetClientDetailsByName(userName);
            }
            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditClient(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                string response = string.Empty;
                if (clientViewModel != null)
                {
                    
                    response = await iclientRepository.UpdateClient(clientViewModel);
                        TempData["DataSavedMessage"] = response;
                    ModelState.Clear();
                }
            }
            return RedirectToAction("ClientDetails",new { userName = clientViewModel.UserName});
        }

        public async Task<IActionResult> AccountDetailsByClient(string userId)
        {
            List<AccountViewModel> accountViewModels = new List<AccountViewModel>();
            
            accountViewModels = await iclientRepository.GetAccountsByClient(userId);
            if(accountViewModels != null)
            return View(accountViewModels);
            else
            {
                TempData["DataSavedMessage"] = "No account found for the client..";
                return RedirectToAction("ClientDetails", new { userName = User.Identity.Name });
            }
        }

        [HttpGet]
        public IActionResult AddClientAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClientAccount(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                string response = string.Empty;
                if (accountViewModel != null)
                {
                    
                    response = await iclientRepository.CreateClientAccount(accountViewModel);
                        TempData["DataSavedMessage"] = response;
                    ModelState.Clear();
                }
            }
            return View();
        }
    }
}
