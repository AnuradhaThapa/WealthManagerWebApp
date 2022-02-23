using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.APIResponse;
using WealthManager.Infrastructure.Model;
using WealthManager.WebApp.API.Interface;

namespace WealthManager.WebApp.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _config;
        private readonly IMapper imapper;
        public UserRepository(IConfiguration config, IMapper mapper)
        {
            _config = config;
            imapper = mapper;
        }
        public async Task<UserDisplayViewModel> GetUserDetailsByName(string userName)
        {
            UserDisplayViewModel userDisplayViewModel = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/GetUserByName/" + userName;
            var response = await ApiResponse.GetResponseAsync(userDisplayViewModel, endpoint);
            if (response != null)
            {
                userDisplayViewModel = imapper.Map<UserDisplayViewModel>(response);
            }
            return userDisplayViewModel;
        }

        public async Task<List<UserViewModel>> GetAgents()
        {
            List<UserViewModel> userViewModels = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Agent/";
            var response = await ApiResponse.GetResponseListAsync(userViewModels, endpoint);
            if (response != null)
            {
                userViewModels = imapper.Map<List<UserViewModel>>(response);
            }
            return userViewModels;
        }

        public async Task<List<UserViewModel>> GetClientsByAgent(string userId)
        {
            List<UserViewModel> userViewModels = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/GetClientByAgent/" + userId;
            var response = await ApiResponse.GetResponseListAsync(userViewModels, endpoint);
            if (response != null)
            {
                userViewModels = imapper.Map<List<UserViewModel>>(response);
            }
            return userViewModels;
        }

        public async Task<string> CreateAgent(UserViewModel userViewModel)
        {
            string response = string.Empty;
            AgentDetails agentDetails = null;
            if(userViewModel != null)
            { 
                agentDetails = imapper.Map<AgentDetails>(userViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Agent/InsertAgentDetails";
                response = await ApiResponse.PostRequestAsync(agentDetails, endpoint);
            }
            return response;
        }

        public async Task<string> CreateUserAuth(UserViewModel userViewModel)
        {
            string message = string.Empty;
            string endpoint = "http://localhost:28681" + "/api/Auth/Create/";
            var response = await ApiResponse.PostRequestAsync(userViewModel, endpoint);
            return response;
        }

        public async Task<string> CreateClient(UserViewModel userViewModel)
        {
            string response = string.Empty;
            ClientDetails clientDetails = null;
            if (userViewModel != null)
            {
                clientDetails = imapper.Map<ClientDetails>(userViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/InsertClientDetails";
                response = await ApiResponse.PostRequestAsync(userViewModel, endpoint);
            }
            return response;
        }

        public async Task<string> UpdateUser(UserDisplayViewModel userDisplayViewModel)
        {
            string response = string.Empty;
            UserDetailsEntity userDetailsEntity = null;
            if (userDisplayViewModel != null)
            {
                userDetailsEntity = imapper.Map<UserDetailsEntity>(userDisplayViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/UpdateUserDetails";
                response = await ApiResponse.PutRequestAsync(userDetailsEntity, endpoint);
            }
            return response;
        }
    }
}
