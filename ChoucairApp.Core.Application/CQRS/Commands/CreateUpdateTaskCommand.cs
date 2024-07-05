using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using ChoucairApp.Core.Domain.Entities;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Commands
{
    public class CreateUpdateTaskCommand : IRequest<IResult<int>>
    {
        public TaskDTO data { get; set; }
    }

    public class CreateUpdateTaskCommandHandler : IRequestHandler<CreateUpdateTaskCommand, IResult<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUpdateTaskCommandHandler(IApplicationDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<IResult<int>> Handle(CreateUpdateTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.data == null)
            {
                return await Result<int>.FailAsync("Ha ocurrido un error en al operación");
            }

            if (request.data.ID == 0)
            {
                TaskEntity task = _mapper.Map<TaskEntity>(request.data);
                _context.Tasks.Add(task);
                await _context.SaveChanges();
                return await Result<int>.SuccessAsync(task.ID, "Se ha creado la tarea con exito");
            }

            var result = _context.Tasks.FirstOrDefault(x => x.ID == request.data.ID);
            if (result != null)
            {
                result.StatusID = request.data.StatusId;
                result.Task_EndDate = request.data.EndDate;
                result.Task_Description = request.data.Descripcion;
                result.Task_Title = request.data.Title;

                _context.Tasks.Update(result);
                await _context.SaveChanges();
                return await Result<int>.SuccessAsync(result.ID);
            }
            else
            {
                return await Result<int>.FailAsync("No se encuentra el registro a actualizar");
            }

        }
    }
}
