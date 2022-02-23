using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Model;

namespace WealthManager.Core.Interface
{
    public interface IClientRepository
    {
        Task<ClientViewModel> GetClientDetailsByName(string userName,string token);
        Task<List<AccountViewModel>> GetAccountsByClient(string clientId, string token);
        Task<string> UpdateClient(ClientViewModel userViewModel, string token);
        Task<string> CreateClientAccount(AccountViewModel accountViewModel, string token);
    }
}
