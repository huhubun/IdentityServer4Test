using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;

namespace Server.Admin.Mappers
{
    public static class ClientScopeMapper
    {
        public static ClientScope ToEntity(this CreateScopeViewModel model)
        {
            return Mapper.Map<ClientScope>(model);
        }
    }
}
