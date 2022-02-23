using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WealthManager.Core.Model;
using WealthManager.WebApp.API.APIResponse;
using WealthManager.WebApp.API.Interface;

namespace WealthManager.WebApp.API.Repository
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

        public async Task<Token> ValidateUserAuth(LoginViewModel loginViewModel)
        {
            Token token = null;
            string endpoint = "http://localhost:28681" + "/api/Auth/Login/";
            var response = await ApiResponse.PostRequestAuthAsync(loginViewModel, token, endpoint);
            if (response != null)
            {
                token = response;
            }
            return token;
        }

        public async Task<UserViewModel> ValidateUser(string userName)
        {
            UserViewModel userViewModel = null;
            string endpoint = _config.GetSection("ApiBaseUrl").Value + "/api/Validation/" + userName;
            var response = await ApiResponse.GetResponseAsync(userViewModel ,endpoint);
            if (response != null)
            {
                userViewModel = imapper.Map<UserViewModel>(response);
            }
            return userViewModel;
        }
    }
}
