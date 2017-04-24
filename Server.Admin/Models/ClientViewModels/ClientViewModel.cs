using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Server.Admin.Models.ClientViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; } = 300;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public bool LogoutSessionRequired { get; set; } = true;
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public bool PrefixClientClaims { get; set; } = true;
        public string LogoutUri { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = ProtocolTypes.OpenIdConnect;
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string LogoUri { get; set; }

        public List<KeyValuePair<int, string>> AllowedScopes { get; set; }
        public List<KeyValuePair<int, string>> RedirectUris { get; set; }
        public List<KeyValuePair<int, string>> IdentityProviderRestrictions { get; set; }
        public List<KeyValuePair<int, string>> AllowedGrantTypes { get; set; }
        public List<KeyValuePair<int, string>> AllowedCorsOrigins { get; set; }
        public List<KeyValuePair<int, string>> PostLogoutRedirectUris { get; set; }
        public List<ClientClaimViewModel> Claims { get; set; }
        public List<ClientSecretViewModel> ClientSecrets { get; set; }
    }
}
