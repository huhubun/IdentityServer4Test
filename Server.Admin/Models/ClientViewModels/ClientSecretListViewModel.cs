using System.Collections.Generic;

namespace Server.Admin.Models.ClientViewModels
{
    public class ClientSecretListViewModel
    {
        public int ClientId { get; set; }

        public List<ClientSecretViewModel> Secrets { get; set; }
    }
}
