using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Queries
{
    public class GetAllStatusesQuery : IRequest<IResult<IEnumerable<StatusesDTO>>> {
    }

    public class GetAllStatusesQueryHandler : IRequestHandler<GetAllStatusesQuery, IResult<IEnumerable<StatusesDTO>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStatusesQueryHandler(IApplicationDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<IResult<IEnumerable<StatusesDTO>>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<List<StatusesDTO>>(_context.Statuses.ToList());
            return await Result<IEnumerable<StatusesDTO>>.SuccessAsync(data.ToList());
        }
    }
}
