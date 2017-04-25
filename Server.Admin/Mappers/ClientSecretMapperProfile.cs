using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Admin.Mappers
{
    public class ClientSecretMapperProfile : Profile
    {
        public ClientSecretMapperProfile()
        {
            //CreateMap<ClientSecretViewModel, ClientSecret>();

            CreateMap<ClientSecret, ClientSecretViewModel>();
        }
    }
}
