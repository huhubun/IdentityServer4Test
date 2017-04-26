using System.Collections.Generic;

namespace Server.Admin.Models.ClientViewModels
{
    public class ClientScopeListViewModel
    {
        public int ClientId { get; set; }

        public Dictionary<int, string> Scopes { get; set; }
    }
}
