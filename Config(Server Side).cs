
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
               
                new Client
                {
                    ClientId = "G T S",
                    ClientSecrets = { new Secret("VE SECRET DEGERI".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                
                  // Login işleminden sonra aktarılacak 
                    RedirectUris = { "http://gts.lojistork.com/signin-oidc" },   // Oidc kütüphanesinden  karmaşık bir adres satırı üretiyor.

                   // Logout işleminden sonra                               
                    PostLogoutRedirectUris = { "http://gts.lojistork.com/Account/Login" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                }
            };
    }
}