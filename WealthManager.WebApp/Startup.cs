using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WealthManager.Infrastructure.Model;
using WealthManager.WebApp.API.Interface;
using WealthManager.WebApp.API.Repository;

namespace WealthManager.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddSession();
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/SignIn/";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvcCore();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            //#region "JWT Token For Authentication Login"    
            Keys.Configure(Configuration.GetSection("AppSettings"));
            var key = Encoding.ASCII.GetBytes(Keys.Token);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(token =>
             {
                 token.RequireHttpsMetadata = false;
                 token.SaveToken = true;
                 token.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = true,
                     ValidIssuer = Keys.WebSiteDomain,
                     ValidateAudience = true,
                     ValidAudience = Keys.WebSiteDomain,
                     RequireExpirationTime = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
             });

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtIssuerOptions:Key"]));

//            var jwtAppSettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
//            services.Configure<JwtIssuerOptions>(options =>
//            {
//                options.Issuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
//                options.Audience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)];
//                options.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            });
//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidIssuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)],
//                ValidateAudience = true,
//                ValidAudience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)],
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = key,
//                RequireExpirationTime = false,
//                ValidateLifetime = true,
//                ClockSkew = TimeSpan.Zero
//            };

//            services.AddAuthentication(options =>
//            {
//                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = tokenValidationParameters;
//    options.Audience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)];
//    options.RequireHttpsMetadata = bool.Parse(jwtAppSettingsOptions[nameof(JwtIssuerOptions.RequireHttpsMetadata)]);
//});

            //#endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseSession();
            //app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRouting();

            //#region "JWT Token For Authentication Login"    

            app.UseCookiePolicy();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            //app.UseAuthentication();
            //app.UseAuthorization();


            //#endregion

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=SignIn}");
            });
        }
    }
}
