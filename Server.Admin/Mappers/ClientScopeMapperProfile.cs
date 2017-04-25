using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public class ClientScopeMapperProfile : Profile
    {
        public ClientScopeMapperProfile()
        {
            CreateMap<CreateScopeViewModel, ClientScope>();
        }
    }
}
