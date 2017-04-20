using System.Collections.Generic;

namespace Server.Admin.Models.ClientViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public bool LogoutSessionRequired { get; set; }
        public int RefreshTokenUsage { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public bool PrefixClientClaims { get; set; }
        public string LogoutUri { get; set; }
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ProtocolType { get; set; }
        public bool RequireClientSecret { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string LogoUri { get; set; }

        public Dictionary<int, string> AllowedScopes { get; set; }
        public Dictionary<int, string> RedirectUris { get; set; }
        public Dictionary<int, string> IdentityProviderRestrictions { get; set; }
        public Dictionary<int, string> AllowedGrantTypes { get; set; }
        public Dictionary<int, string> AllowedCorsOrigins { get; set; }
        public Dictionary<int, string> PostLogoutRedirectUris { get; set; }
        public List<ClientClaimViewModel> Claims { get; set; }
        public List<ClientSecretViewModel> ClientSecrets { get; set; }
    }
}
