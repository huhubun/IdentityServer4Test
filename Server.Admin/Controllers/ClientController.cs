using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Admin.Mappers;
using Server.Admin.Models.ClientViewModels;
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
            return View();
        }

        //[HttpPost]
        //public IActionResult Add(ClientViewModel client)
        //{
        //    var clientEntity = client.ToEntity();
        //    _configurationDbContext.Clients.Add(clientEntity);
        //    _configurationDbContext.SaveChanges();

        //    return RedirectToAction(nameof(Edit), new { id = clientEntity.Id });
        //}

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

    }
}