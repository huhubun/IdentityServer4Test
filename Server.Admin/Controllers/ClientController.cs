using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Admin.Mappers;
using Server.Admin.Models.ClientViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Server.Admin.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public ClientController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Clients = _configurationDbContext.Clients.ToList();

            return View();
        }

        public IActionResult Add()
        {
            var model = new ClientViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ClientViewModel client)
        {
            var clientEntity = client.ToEntity();
            _configurationDbContext.Clients.Add(clientEntity);
            _configurationDbContext.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = clientEntity.Id });
        }

        public IActionResult Edit(int id)
        {
            var client = _configurationDbContext.Clients.Include(c => c.AllowedGrantTypes).Single(c => c.Id == id);

            var model = client.ToModel();

            return View(model);



            //return View(Mapper.Map<ClientViewModel>(client.ToModel()));
        }

        [HttpPost]
        public IActionResult Edit(ClientViewModel client)
        {
            var clientEntity = _configurationDbContext.Clients.Include(c => c.AllowedGrantTypes).Single(c => c.Id == client.Id);

            clientEntity = Mapper.Map<ClientViewModel, Client>(client, clientEntity);

            return View();

            //var clientEntity = _configurationDbContext.Clients.Find(client.Id);

            //clientEntity.ClientName = client.ClientName;

            //_configurationDbContext.SaveChanges();

            //return RedirectToAction(nameof(Edit), new { id = clientEntity.Id });
        }


        #region ClientScopes

        [HttpGet]
        public IActionResult CreateScope(int clientId)
        {
            var model = new CreateScopeViewModel { ClientId = clientId };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateScope(CreateScopeViewModel model)
        {
            var entity = model.ToEntity();

            var client = _configurationDbContext.Clients.Include(c => c.AllowedScopes).Single(c => c.Id == model.ClientId);
            client.AllowedScopes.Add(entity);
            _configurationDbContext.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = model.ClientId });
        }

        [HttpGet]
        public IActionResult DeleteScope(int clientId, int id)
        {
            var scopeSet = _configurationDbContext.Set<ClientScope>();
            scopeSet.Remove(scopeSet.Find(id));
            _configurationDbContext.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = clientId });
        }

        #endregion

        #region ClientSecret

        [HttpGet]
        public IActionResult CreateSecret(int clientId)
        {
            var model = new CreateSecretViewModel { ClientId = clientId };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateSecret(CreateSecretViewModel model)
        {
            var entity = model.ToEntity();

            var client = _configurationDbContext.Clients.Include(c => c.ClientSecrets).Single(c => c.Id == model.ClientId);
            client.ClientSecrets.Add(entity);
            _configurationDbContext.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = model.ClientId });
        }

        [HttpGet]
        public IActionResult DeleteSecret(int clientId, int id)
        {
            var secretSet = _configurationDbContext.Set<ClientSecret>();
            secretSet.Remove(secretSet.Find(id));
            _configurationDbContext.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = clientId });
        }

        #endregion

    }
}