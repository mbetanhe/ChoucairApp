using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Commands
{

    public class CompletedTaskCommand : IRequest<IResult<bool>>
    {
        public int Id { get; set; }
    }

    public class CompletedTaskCommandHandler : IRequestHandler<CompletedTaskCommand, IResult<bool>>
    {
        private readonly IApplicationDbContext _context;

        public CompletedTaskCommandHandler(IApplicationDbContext context) => (_context) = (context);

        public async Task<IResult<bool>> Handle(CompletedTaskCommand request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
                return Result<bool>.Fail("El identificador se encuentra vacío");

            var data = _context.Tasks.Where(e => e.ID == request.Id).FirstOrDefault();

            if (data != null)
            {
                data.StatusID = 3;
                _context.Tasks.Update(data);
                await _context.SaveChanges();

                return Result<bool>.Success(true, "Se ha completado la tarea");
            }

            return Result<bool>.Fail("La tarea no se pudo completar");
        }
    }
}
