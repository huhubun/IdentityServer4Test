using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return View(client.ToModel() as ClientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            return View();
        }

    }
}