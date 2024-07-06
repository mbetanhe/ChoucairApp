using ChoucairApp.Core.Application.DTOs.Identity;
using ChoucairApp.Core.Application.Enums;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Interfaces.Identity;
using ChoucairApp.Core.Application.Models;
using ChoucairApp.Core.Application.Responses;
using ChoucairApp.Core.Domain.Settings;
using ChoucairApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ChoucairApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;
        private readonly IApplicationDbContextSeed _contextSeed;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwt, IApplicationDbContextSeed contextSeed   )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _contextSeed = contextSeed;
        }

        public async Task<IResult<string>> RegisterAsync(RegisterDTO data)
        {
            var user = new ApplicationUser
            {
                UserName = data.UserName,
                Email = data.Email,
                FirstName = data.Firsname,
                LastName = data.Lastname,
                Document = data.Document,  
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(data.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, data.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ERoles.User.ToString());

                }
                return Result<string>.Success(user.Id,$"Usuario registrado con cuenta {user.UserName}");
            }
            else
            {
                return Result<string>.Fail($"Ya existe el usuario");
            }
        }

        public async Task SeedDb()
        {
            await _contextSeed.SeedEssentialsAsync(_userManager, _roleManager);
        }

        public async Task<AuthenticationResponse> GetTokenAsync(LoginDTO data)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null)
            {
                authenticationResponse.IsAuthenticated = false;
                authenticationResponse.Message = $"No existe el usuario {data.Email}.";
                // return Result<AuthenticationResponse>.Success(authenticationResponse);
                return authenticationResponse;

            }
            if (await _userManager.CheckPasswordAsync(user, data.Password))
            {
                authenticationResponse.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationResponse.Email = user.Email;
                authenticationResponse.Id = user.Id;
                authenticationResponse.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationResponse.Roles = rolesList.ToList();
                //return Result<AuthenticationResponse>.Success(authenticationResponse);
                return authenticationResponse;
            }
            authenticationResponse.IsAuthenticated = false;
            authenticationResponse.Message = $"Credenciales incorrectas para {user.Email}.";
            //return Result<AuthenticationResponse>.Success(authenticationResponse);

            return authenticationResponse;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
