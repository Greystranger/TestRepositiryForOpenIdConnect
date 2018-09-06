using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerQuickStartApp
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var apiResources = new List<ApiResource>
            {
                new ApiResource("api_one", "My API")
            };

            return apiResources;
        }

        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client()
                {
                    ClientId = "webapi_client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"api_one"}
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "api_one" }
                },

                //OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc_client",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //where to redirect to after Login
                    RedirectUris = { @"https://localhost:5005/signin-oidc" },

                    //where to redirect to after Logout
                    PostLogoutRedirectUris = { @"https://localhost:5005/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api_one"
                    },

                    AllowOfflineAccess = true
                }
            };

            return clients;
        }

        public static List<TestUser> GetUsers()
        {
            var testUsers = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Alice",
                    Password = "password123"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "John Wayne",
                    Password = "good_dead_indsman",

                    Claims = new []
                    {
                        new Claim("name", "John Wayne"),
                        new Claim("website", "https://bestwesterns.com") 
                    }
                }
            };

            return testUsers;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

            return identityResources;
        }
    }
}
