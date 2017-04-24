using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Admin.Models.ClientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Admin.ViewComponents
{
    public class ClientScopeViewComponent : ViewComponent
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public ClientScopeViewComponent(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public IViewComponentResult Invoke(int clientId)
        {
            var client = _configurationDbContext.Clients.Include(c => c.AllowedScopes).First(c => c.Id == clientId);

            var model = new ClientScopeListViewModel
            {
                ClientId = clientId,
                Scopes = client.AllowedScopes.ToDictionary(s => s.Id, s => s.Scope)
            };

            return View("~/Views/Client/ClientScopeList.cshtml", model);
        }
    }
}
