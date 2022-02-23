using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.WebApp.API.Interface;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.APIResponse;
using WealthManager.Infrastructure.Model;

namespace WealthManager.WebApp.API.Repository
{
    public class ClientRepository: IClientRepository
    {
        private IConfiguration _config;
        private readonly IMapper imapper;
        public ClientRepository(IConfiguration config, IMapper mapper)
        {
            _config = config;
            imapper = mapper;
        }

        public async Task<ClientViewModel> GetClientDetailsByName(string userName)
        {
            ClientViewModel clientViewModel = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/GetClientByName/" + userName;
            var response = await ApiResponse.GetResponseAsync(clientViewModel, endpoint);
            if (response != null)
            {
                clientViewModel = imapper.Map<ClientViewModel>(response);
            }
            return clientViewModel;
        }

        public async Task<string> UpdateClient(ClientViewModel clientViewModel)
        {
            string response = string.Empty;
            ClientDetailsEntity clientDetails = null;
            if (clientViewModel != null)
            {
                clientDetails = imapper.Map<ClientDetailsEntity>(clientViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/UpdateClientDetails";
                response = await ApiResponse.PutRequestAsync(clientDetails, endpoint);
            }
            return response;
        }

        public async Task<List<AccountViewModel>> GetAccountsByClient(string clientId)
        {
            List<AccountViewModel> accountViewModels = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/AccountDetailByClientID/" + clientId;
            var response = await ApiResponse.GetResponseListAsync(accountViewModels, endpoint);
            if (response != null)
            {
                accountViewModels = imapper.Map<List<AccountViewModel>>(response);
            }
            return accountViewModels;
        }

        public async Task<string> CreateClientAccount(AccountViewModel accountViewModel)
        {
            string response = string.Empty;
            AccountDetails accountDetails = null;
            if (accountViewModel != null)
            {
                accountViewModel.ClientID = accountViewModel.ClientID;
                accountDetails = imapper.Map<AccountDetails>(accountViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/InsertClientAccountDetails";
                response = await ApiResponse.PostRequestAsync(accountDetails, endpoint);
            }
            return response;
        }
    }
}
