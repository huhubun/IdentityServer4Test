using System;
using static IdentityServer4.IdentityServerConstants;

namespace Server.Admin.Models.ClientViewModels
{
    public class CreateSecretViewModel
    {
        public int ClientId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = SecretTypes.SharedSecret;
    }
}
