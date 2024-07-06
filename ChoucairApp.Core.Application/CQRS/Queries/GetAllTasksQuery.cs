using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Responses;
using MediatR;

namespace ChoucairApp.Core.Application.CQRS.Queries
{
    public class GetAllTasksQuery : IRequest<IResult<IEnumerable<TaskDTO>>> {
        public string UserId { get; set; }
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IResult<IEnumerable<TaskDTO>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IMapper mapper, IApplicationDbContext context) =>  (_mapper, _context) = (mapper, context);

        public async Task<IResult<IEnumerable<TaskDTO>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<TaskDTO> data = from tk in _context.Tasks.ToList()
                       join st in _context.Statuses.ToList() on tk.StatusID equals st.ID
                       where tk.UserID == request.UserId
                       select new TaskDTO()
                       {
                           ID = tk.ID,
                           Descripcion = tk.Task_Description,
                           Title = tk.Task_Title,
                           EndDate = tk.Task_EndDate,
                           StatusDesc = st.Status_Description,
                           StatusId = tk.StatusID
                       };
            
            return await Result<IEnumerable<TaskDTO>>.SuccessAsync(data.ToList());
        }
    }
}
