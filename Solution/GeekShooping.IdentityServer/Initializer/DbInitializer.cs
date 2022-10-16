using GeekShooping.IdentityServer.Configuration;
using GeekShooping.IdentityServer.Model;
using GeekShooping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShooping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySQLContext context,
                             // USUARIO
                             UserManager<ApplicationUser> user,
                             // Papel
                             RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initializer()
        {

            //verificando  se tem usuário como admim se não tiver eu não precisar persistir os dados porque já tem
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "felipe-admin",
                Email = "felipe-27@hotmail.com",
                EmailConfirmed = true,
                PhoneNumber = "17 98120-5099",
                FirstName = "Felipe",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Felpe123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();


            var adminClaims = _user.AddClaimsAsync(admin, new Claim[] {

                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} ${admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "felipe-client",
                Email = "felipe-27@hotmail.com",
                EmailConfirmed = true,
                PhoneNumber = "17 98120-5099",
                FirstName = "Felipe",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Felpe123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();


            var clientClaims = _user.AddClaimsAsync(client, new Claim[] {

                new Claim(JwtClaimTypes.Name, $"{client.FirstName} ${client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
            }).Result;
        }
    }
}
