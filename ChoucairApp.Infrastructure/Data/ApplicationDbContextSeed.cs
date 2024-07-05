using ChoucairApp.Core.Application.Enums;
using ChoucairApp.Core.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace ChoucairApp.Infrastructure.Data
{
    public interface IApplicationDbContextSeed
    {
        Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager);
    }
    public class ApplicationDbContextSeed : IApplicationDbContextSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityRole _identityRole;

        public ApplicationDbContextSeed(UserManager<ApplicationUser> userManager, IdentityRole identityRole) => (_userManager, _identityRole) = (userManager, identityRole);

        public async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Alimentamos los roles
            await roleManager.CreateAsync(new IdentityRole(ERoles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERoles.User.ToString()));

            //Creamos un usuario por defecto
            var defaultUser = new ApplicationUser
            {
                UserName = "UserPrueba",
                Email = "userprueba@email.com",
                Document = "12345679",
                FirstName = "Usuario",
                LastName = "Prueba",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Pa$$w0rd.");
                await userManager.AddToRoleAsync(defaultUser, ERoles.User.ToString());
            }
        }
    }
}
