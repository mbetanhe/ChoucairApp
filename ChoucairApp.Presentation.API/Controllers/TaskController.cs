using ChoucairApp.Core.Application.CQRS.Commands;
using ChoucairApp.Core.Application.CQRS.Queries;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChoucairApp.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : BaseApiController
    {
        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<TaskDTO>>))]
        public async Task<IActionResult> GetAll(string userId)
        {
            return Ok(await Mediator.Send(new GetAllTasksQuery() { UserId = userId }));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<int>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> Create(TaskDTO request)
        {
            var result = await Mediator.Send(new CreateUpdateTaskCommand()
            {
                data = request
            });

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<bool>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteTaskByIDCommand()
            {
                ID = id
            });

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("CompletedTask")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<bool>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(IResult<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IResult<bool>))]
        public async Task<IActionResult> CompletedTask([FromBody] int id)
        {
            var result = await Mediator.Send(new CompletedTaskCommand() { Id = id });
            if (result.Succeeded)   
                return Ok(result);  

            else return BadRequest(result); 
        }
    }
}

