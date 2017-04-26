using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public class ClientSecretMapperProfile : Profile
    {
        public ClientSecretMapperProfile()
        {
            CreateMap<CreateSecretViewModel, ClientSecret>()
                .ForMember(dest => dest.Value, mo => mo.MapFrom(src => src.Value.Sha256()));

            CreateMap<ClientSecret, ClientSecretViewModel>();
        }
    }
}
