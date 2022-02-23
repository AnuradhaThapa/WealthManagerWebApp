using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Model;

namespace WealthManager.Core.Interface
{
    public interface IUserRepository
    {
        Task<UserDisplayViewModel> GetUserDetailsByName(string userName,string token);
        Task<List<UserViewModel>> GetAgents(string token);
        Task<List<UserViewModel>> GetClientsByAgent(string userId,string token);
        Task<string> CreateAgent(UserViewModel userViewModel,string token);
        Task<string> CreateClient(UserViewModel userViewModel,string token);
        Task<string> UpdateUser(UserDisplayViewModel userDisplayViewModel, string token);
    }
}
