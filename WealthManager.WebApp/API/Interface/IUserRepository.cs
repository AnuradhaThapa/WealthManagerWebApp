using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Model;

namespace WealthManager.WebApp.API.Interface
{
    public interface IUserRepository
    {
        Task<UserDisplayViewModel> GetUserDetailsByName(string userName);
        Task<List<UserViewModel>> GetAgents();
        Task<List<UserViewModel>> GetClientsByAgent(string userId);
        Task<string> CreateAgent(UserViewModel userViewModel);
        Task<string> CreateClient(UserViewModel userViewModel);
        Task<string> UpdateUser(UserDisplayViewModel userDisplayViewModel);
        Task<string> CreateUserAuth(UserViewModel userViewModel);
    }
}
