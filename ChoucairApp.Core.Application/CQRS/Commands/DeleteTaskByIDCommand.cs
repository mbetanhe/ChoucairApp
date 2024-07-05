using AutoMapper;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Commands
{
    public class DeleteTaskByIDCommand : IRequest<IResult<bool>>
    {
        public int ID { get; set; }
    }

    public class DeleteTaskByIDCommandHandler : IRequestHandler<DeleteTaskByIDCommand, IResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteTaskByIDCommandHandler(IMapper mapper, IApplicationDbContext context) => (_mapper, _context) = (mapper, context);

        public async Task<IResult<bool>> Handle(DeleteTaskByIDCommand request, CancellationToken cancellationToken)
        {
            if(request.ID <= 0)
                await Result<bool>.FailAsync("El identificador no puede ser cero");

            var data = _context.Tasks.Where(x => x.ID == request.ID).FirstOrDefault();

            if (data != null)
            {
                _context.Tasks.Remove(data);
                await _context.SaveChanges();
                return Result<bool>.Success(true, "Se ha eliminado la tarea con exito");
            }

            return await Result<bool>.FailAsync("No existe el registro a eliminar");
        }
    }
}
