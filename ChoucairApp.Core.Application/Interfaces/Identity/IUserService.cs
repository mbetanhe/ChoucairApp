using ChoucairApp.Core.Application.DTOs.Identity;
using ChoucairApp.Core.Application.Responses;

namespace ChoucairApp.Core.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task SeedDb();

        Task<IResult<string>> RegisterAsync(RegisterDTO model);

        Task<AuthenticationResponse> GetTokenAsync(LoginDTO data);
    }
}
