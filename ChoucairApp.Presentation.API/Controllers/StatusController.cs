using ChoucairApp.Core.Application.CQRS.Queries;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChoucairApp.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<StatusesDTO>>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllStatusesQuery()));
        }
    }
}
