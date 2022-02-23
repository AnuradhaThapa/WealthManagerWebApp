using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WealthManager.Core.Model;

namespace WealthManager.Core.Interface
{
    public interface ILoginRepository
    {
        //Task<Token> ValidateUserAuth(LoginViewModel loginViewModel);
        Task<UserViewModel> ValidateUser(LoginViewModel loginViewModel);
    }
}
