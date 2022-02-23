using AutoMapper;
using WealthManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace WealthManager.Infrastructure.Model
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserDetailsEntity, UserViewModel>();
            CreateMap<UserViewModel, UserDetailsEntity>();

            CreateMap<UserDetailsEntity, UserDisplayViewModel>();
            CreateMap<UserDisplayViewModel, UserDetailsEntity>();

            CreateMap<AgentDetails, UserViewModel>();
            CreateMap<UserViewModel, AgentDetails>();

            CreateMap<ClientDetailsEntity, ClientViewModel>();
            CreateMap<ClientViewModel, ClientDetailsEntity>();

            CreateMap<ClientDetails, UserViewModel>();
            CreateMap<UserViewModel, ClientDetails>();

            CreateMap<AccountDetails, AccountViewModel>();
            CreateMap<AccountViewModel, AccountDetails>();
        }
    }
}
