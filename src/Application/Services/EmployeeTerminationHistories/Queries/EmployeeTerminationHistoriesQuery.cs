using Engage.Application.Services.EmployeeTerminationHistories.Models;

namespace Engage.Application.Services.EmployeeTerminationHistories.Queries
{
    public class EmployeeTerminationHistoriesQuery : GetQuery, IRequest<ListResult<EmployeeTerminationHistoryDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeTerminationHistoriesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeTerminationHistoriesQuery, ListResult<EmployeeTerminationHistoryDto>>
    {
        public EmployeeTerminationHistoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeTerminationHistoryDto>> Handle(EmployeeTerminationHistoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeTerminationHistories.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeTerminationHistoryId)
                                                           .ProjectTo<EmployeeTerminationHistoryDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeTerminationHistoryDto>(entities);
        }
    }
}
