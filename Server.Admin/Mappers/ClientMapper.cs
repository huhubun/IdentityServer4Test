using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public static class ClientMapper
    {
        public static ClientViewModel ToModel(this Client client)
        {
            return Mapper.Map<ClientViewModel>(client);
        }

        public static Client ToEntity(this ClientViewModel model)
        {
            return Mapper.Map<Client>(model);
        }
    }
}
