using Engage.Application.Services.EmployeeLeaveEntries.Models;

namespace Engage.Application.Services.EmployeeLeaveEntries.Queries;

public class EmployeeLeaveEntriesQuery : GetQuery, IRequest<ListResult<EmployeeLeaveEntryDto>>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeLeaveEntryListQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeLeaveEntriesQuery, ListResult<EmployeeLeaveEntryDto>>
{
    public GetEmployeeLeaveEntryListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeLeaveEntryDto>> Handle(EmployeeLeaveEntriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeLeaveEntries.Where(e => e.EmployeeId == request.EmployeeId)
                                                          .OrderBy(e => e.FromDate)
                                                          .ProjectTo<EmployeeLeaveEntryDto>(_mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        return new ListResult<EmployeeLeaveEntryDto>(entities);
    }
}
