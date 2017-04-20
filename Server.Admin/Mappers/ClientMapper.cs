using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Server.Admin.Models.ClientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Admin.Mappers
{
    public static class ClientMapper
    {
        public static ClientViewModel ToModel(this Client client)
        {
            return Mapper.Map<ClientViewModel>(client);
        }
    }
}
