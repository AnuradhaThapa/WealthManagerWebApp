using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WealthManager.WebApp.Authorize
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = string.Empty,userId = string.Empty;
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var queryString = context.HttpContext.Request.QueryString;
            if(queryString.HasValue && queryString.Value.Contains("userName"))
             userName = queryString.ToString().Split("=")[1];
            if (queryString.HasValue && queryString.Value.Contains("userId"))
                userId = queryString.ToString().Split("=")[1];

            //First get user claims    
            var claims = context.HttpContext.User.Claims;

            //Filter specific claim    
            string UserId = claims?.FirstOrDefault(x => x.Type.Equals("userId", StringComparison.OrdinalIgnoreCase))?.Value;
            string UserName = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value;


            if (IsAuthenticated)
            {

                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    string[] roles = item.Split(",");
                    foreach (var role in roles)
                    {                     
                        if (context.HttpContext.User.HasClaim("Role", role)
                           && (context.HttpContext.User.HasClaim(ClaimTypes.Name, !string.IsNullOrEmpty(userName) ? userName : UserName))
                           && (context.HttpContext.User.HasClaim("UserId", !string.IsNullOrEmpty(userId) ? userId : UserId)))
                            flagClaim = true;
                    }
                }
                if (!flagClaim)
                {
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Set HTTP 401   
                    else
                    {
                        context.HttpContext.Session.Clear();
                        context.Result = new RedirectResult("~/Login/SignIn");
                    }
                }
            }
            else
            {
                context.HttpContext.Session.Clear();
                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Set HTTP 403 -   
                }
                else
                {
                    context.HttpContext.Session.Clear();
                    context.Result = new RedirectResult("~/Login/SignIn");
                }
            }
            return;
        }
    }
}