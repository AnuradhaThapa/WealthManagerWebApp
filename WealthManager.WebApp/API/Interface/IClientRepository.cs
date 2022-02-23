using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Model;

namespace WealthManager.WebApp.API.Interface
{
    public interface IClientRepository
    {
        Task<ClientViewModel> GetClientDetailsByName(string userName);
        Task<List<AccountViewModel>> GetAccountsByClient(string clientId);
        Task<string> UpdateClient(ClientViewModel userViewModel);
        Task<string> CreateClientAccount(AccountViewModel accountViewModel);
    }
}
