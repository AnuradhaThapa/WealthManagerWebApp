using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Interface;
using WealthManager.Core.Model;
using WealthManager.Infrastructure.APIResponse;
using WealthManager.Infrastructure.Model;

namespace WealthManager.Infrastructure.Repository
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
        public async Task<UserDisplayViewModel> GetUserDetailsByName(string userName,string token)
        {
            UserDisplayViewModel userDisplayViewModel = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/GetUserByName/" + userName;
            var response = await ApiResponse.GetResponseAsync(userDisplayViewModel, endpoint,token);
            if (response != null)
            {
                userDisplayViewModel = imapper.Map<UserDisplayViewModel>(response);
            }
            return userDisplayViewModel;
        }

        public async Task<List<UserViewModel>> GetAgents(string token)
        {
            List<UserViewModel> userViewModels = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Agent/";
            var response = await ApiResponse.GetResponseListAsync(userViewModels, endpoint, token);
            if (response != null)
            {
                userViewModels = imapper.Map<List<UserViewModel>>(response);
            }
            return userViewModels;
        }

        public async Task<List<UserViewModel>> GetClientsByAgent(string userId,string token)
        {
            List<UserViewModel> userViewModels = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/GetClientByAgent/" + userId;
            var response = await ApiResponse.GetResponseListAsync(userViewModels, endpoint,token);
            if (response != null)
            {
                userViewModels = imapper.Map<List<UserViewModel>>(response);
            }
            return userViewModels;
        }

        public async Task<string> CreateAgent(UserViewModel userViewModel,string token)
        {
            string response = string.Empty;
            AgentDetails agentDetails = null;
            if(userViewModel != null)
            { 
                agentDetails = imapper.Map<AgentDetails>(userViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Agent/InsertAgentDetails";
                response = await ApiResponse.PostRequestAsync(agentDetails, endpoint,token);
            }
            return response;
        }

        public async Task<string> CreateClient(UserViewModel userViewModel,string token)
        {
            string response = string.Empty;
            if (userViewModel != null)
            {
                userViewModel.AgentId = userViewModel.AgentId;
                //clientDetails = imapper.Map<ClientDetails>(userViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Client/InsertClientDetails";
                response = await ApiResponse.PostRequestAsync(userViewModel, endpoint,token);
            }
            return response;
        }

        public async Task<string> UpdateUser(UserDisplayViewModel userDisplayViewModel, string token)
        {
            string response = string.Empty;
            UserDetailsEntity userDetailsEntity = null;
            if (userDisplayViewModel != null)
            {
                userDetailsEntity = imapper.Map<UserDetailsEntity>(userDisplayViewModel);
                string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/UpdateUserDetails";
                response = await ApiResponse.PutRequestAsync(userDetailsEntity, endpoint, token);
            }
            return response;
        }
    }
}
