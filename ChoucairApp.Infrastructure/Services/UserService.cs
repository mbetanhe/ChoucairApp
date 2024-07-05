using ChoucairApp.Core.Application.Interfaces.Identity;
using ChoucairApp.Core.Application.Models;
using ChoucairApp.Core.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ChoucairApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
    }
}
