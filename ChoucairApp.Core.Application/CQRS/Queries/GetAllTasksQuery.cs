using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Queries
{
    public class GetAllTasksQuery : IRequest<IResult<IEnumerable<TaskDTO>>> { }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IResult<IEnumerable<TaskDTO>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IMapper mapper, IApplicationDbContext context) =>  (_mapper, _context) = (mapper, context);

        public async Task<IResult<IEnumerable<TaskDTO>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<List<TaskDTO>>(_context.Tasks.ToList());
            return await Result<IEnumerable<TaskDTO>>.SuccessAsync(data.ToList());
        }
    }
}
