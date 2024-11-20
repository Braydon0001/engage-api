using Engage.Application.Services.EmployeeReinstatementHistories.Models;

namespace Engage.Application.Services.EmployeeReinstatementHistories.Queries
{
    public class EmployeeReinstatementHistoriesQuery : GetQuery, IRequest<ListResult<EmployeeReinstatementHistoryDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeReinstatementHistoriesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeReinstatementHistoriesQuery, ListResult<EmployeeReinstatementHistoryDto>>
    {
        public EmployeeReinstatementHistoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeReinstatementHistoryDto>> Handle(EmployeeReinstatementHistoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeReinstatementHistories.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeReinstatementHistoryId)
                                                           .ProjectTo<EmployeeReinstatementHistoryDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeReinstatementHistoryDto>(entities);
        }
    }
}
