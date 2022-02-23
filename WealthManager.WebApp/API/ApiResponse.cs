using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WealthManager.WebApp.API.APIResponse
{
    public class ApiResponse
    {
        private static string token;
        private HttpContext Context { get; set; }

        public ApiResponse(HttpContext context)
        {
            this.Context = context;
            GetToken();
        }

        public string GetToken()
        {
            token = this.Context.Session.GetString("Token");
            return token;
        }
        public static async Task<T2> PostRequestAuthAsync<T1, T2>(T1 request, T2 responseData, string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data = await Response.Content.ReadAsStringAsync();
                        responseData = JsonConvert.DeserializeObject<T2>(data);
                    }
                }
            }
            return responseData;
        }

        public static async Task<string> PutRequestAsync<T1>(T1 request, string endpoint)
        {
            string message = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                 (JwtBearerDefaults.AuthenticationScheme, token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var Response = await client.PutAsync(endpoint, content))
                {
                    message = await Response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }

        public static async Task<string> PostRequestAsync<T1>(T1 request, string endpoint)
        {
            string message = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                 (JwtBearerDefaults.AuthenticationScheme, token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    message = await Response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }

        public static async Task<T1> GetResponseAsync<T1>(T1 responseData, string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                      (JwtBearerDefaults.AuthenticationScheme, token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data = await Response.Content.ReadAsStringAsync();
                        responseData = JsonConvert.DeserializeObject<T1>(data);
                    }
                }
            }
            return responseData;
        }

        public static async Task<List<T1>> GetResponseListAsync<T1>(List<T1> responseData, string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                        (JwtBearerDefaults.AuthenticationScheme, token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var data = await Response.Content.ReadAsStringAsync();
                        responseData = JsonConvert.DeserializeObject<List<T1>>(data);
                    }
                }
            }
            return responseData;
        }
    }
}
