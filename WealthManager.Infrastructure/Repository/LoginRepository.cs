using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WealthManager.Core.Interface;
using WealthManager.Core.Model;
using WealthManager.Infrastructure.APIResponse;
using WealthManager.Infrastructure.Model;

namespace WealthManager.Infrastructure.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly IMapper imapper;
        private IConfiguration _config;
        public LoginRepository(IMapper mapper,IConfiguration config)
        {
            imapper = mapper;
            _config = config;
        }

        //public async Task<Token> ValidateUserAuth(LoginViewModel loginViewModel) 
        //{
        //    Token token = null;
        //    string endpoint = "http://localhost:28681" + "/api/Auth/Login/";
        //    var response = await ApiResponse.PostRequestAsync(loginViewModel, token, endpoint);
        //    if (response != null)
        //    {
        //        token = response;
        //    }
        //    return token;
        //}

        public async Task<UserViewModel> ValidateUser(LoginViewModel loginViewModel)
        {
            UserViewModel userViewModel = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/";
            var response = await ApiResponse.PostRequestValidateAsync(loginViewModel,userViewModel ,endpoint);
            if (response != null)
            {
                userViewModel = imapper.Map<UserViewModel>(response);
            }
            return userViewModel;
        }
    }
}
