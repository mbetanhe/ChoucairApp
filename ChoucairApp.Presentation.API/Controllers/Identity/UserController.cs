using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.DTOs.Identity;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Interfaces.Identity;
using ChoucairApp.Core.Application.Responses;
using ChoucairApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace ChoucairApp.Presentation.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       

        public UserController(IUserService userService) => (_userService) = userService;

        [HttpGet("Seed")]
        public async Task<IActionResult> Seed()
        {
            await _userService.SeedDb();
            return Ok();
        }

        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<string>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<string>))]
        public async Task<ActionResult> RegisterAsync(RegisterDTO request)
        {
            var result = await _userService.RegisterAsync(request);
            return Ok(result);
        }


        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<AuthenticationResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<string>))]
        public async Task<IActionResult> GetTokenAsync(LoginDTO request)
        {
            var result = await _userService.GetTokenAsync(request);
            return Ok(result);
        }

    }
}
