using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using GeekShooping.IdentityServer.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShooping.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // criando as informações para JWT
            string id = context.Subject.GetSubjectId();
            // pesquisando as informações do usuárip
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            // criando a Claim
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
            // transformando em uma lista a claims
            List<Claim> claims = userClaims.Claims.ToList();

            // adicionando
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            // verificando se é aceitar suporte para roles
            if(_userManager.SupportsUserRole)
            {
                // pegando todas as roles do usuário
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));
                        // verificando se suporte role na clai;
                    if(_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                        // adicionando na claim
                        if (identityRole != null) claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }

                }
                // adicionando a claims para contexto
                context.IssuedClaims = claims;
            }
    
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            // verificando se o usário está ativo
            string id = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            context.IsActive = user != null;
        }
    }
}
