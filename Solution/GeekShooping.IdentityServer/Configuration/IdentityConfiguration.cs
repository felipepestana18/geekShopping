using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShooping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        // Permissão no sitema
        public const string Admin = "Admin";
        public const string Client = "Client";

        // Identity Resource -  é grupo do identity
        // Dados a ser protegido do identity server como dados do Usuário
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Email(),
               new IdentityResources.Profile()

            };

        // Identity Scope são os recurso que o usuário poderá acessar
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("geek_shopping", "GeekShooping Server"),
            new ApiScope(name: "read", "Read Data."),
            new ApiScope(name: "write", "Write Data."),
            new ApiScope(name: "delete", "Delete Data."),
        };
        // Client é um componete de software (por exemplo front-end) que solicitar uma token para identity serve
        // ele pode permitir ou nega acesso a um recurso
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = {new Secret("my_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}
            },
            new Client
            {
                ClientId = "geek_shopping",
                ClientSecrets = {new Secret("my_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                // Redireciona a tela de Lagou out Login
                // Para pegar a URL front-end GeekShooping.Wev Propeties laucnSettings.json
                // Adiocionar /signin-oidc e /sigout-callback-oidc
                RedirectUris = {"https://localhost:4430/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "geek_shopping"
                }
            }

        };
    }
}
