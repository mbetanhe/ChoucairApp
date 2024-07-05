using ChoucairApp.Core.Application.CQRS.Commands;
using ChoucairApp.Core.Application.CQRS.Queries;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChoucairApp.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<TaskDTO>>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTasksQuery()));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IResult<IEnumerable<StatusesDTO>>))]
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

            if(result.Succeeded)
                return Ok(result);  

            return BadRequest(result);
        }
    }
}
