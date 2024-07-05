using ChoucairApp.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChoucairApp.Presentation.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;


    }
}
