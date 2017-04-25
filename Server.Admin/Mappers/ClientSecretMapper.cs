using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public static class ClientSecretMapper
    {
        public static ClientSecretViewModel ToModel(this ClientSecret entity)
        {
            return Mapper.Map<ClientSecretViewModel>(entity);
        }
    }
}
