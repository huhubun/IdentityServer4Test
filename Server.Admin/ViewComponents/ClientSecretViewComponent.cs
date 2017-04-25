using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Admin.Models.ClientViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Server.Admin.ViewComponents
{
    public class ClientSecretViewComponent : ViewComponent
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public ClientSecretViewComponent(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public IViewComponentResult Invoke(int clientId)
        {
            var secretSet = _configurationDbContext.Set<ClientSecret>();
            var secrets = secretSet.Where(s => s.Client.Id == clientId);

            var model = new ClientSecretListViewModel
            {
                ClientId = clientId,
                Secrets = Mapper.Map<List<ClientSecretViewModel>>(secrets)
            };

            return View("~/Views/Client/ClientSecretList.cshtml", model);
        }
    }
}
