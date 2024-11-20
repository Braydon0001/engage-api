using Engage.Application.Services.NotificationEmployeeReads.Models;
using System.Linq.Dynamic.Core;

namespace Engage.Application.Services.NotificationEmployeeReads.Queries;

public class GetEmployeeNotificationReadsQuery : GetQuery, IRequest<ListResult<EmployeeNotificationReadsDto>>
{
    public int EmployeeId { get; set; }
}



public class GetEmployeeNotificationReadsQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeNotificationReadsQuery, ListResult<EmployeeNotificationReadsDto>>
{
    private readonly IDateTimeService _date;

    public GetEmployeeNotificationReadsQueryHandler(IAppDbContext context, IMapper mapper, IDateTimeService date) : base(context, mapper)
    {
        _date = date;
    }

    public async Task<ListResult<EmployeeNotificationReadsDto>> Handle(GetEmployeeNotificationReadsQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.NotificationEmployeeReads.AsQueryable();

        var employeeNotificationReads = await queryable.ProjectTo<EmployeeNotificationReadsDto>(_mapper.ConfigurationProvider)
            .Where(e => e.EmployeeId == query.EmployeeId && e.NotificationEndDate >= _date.Now.Date)
            .ToListAsync(cancellationToken);

        return new ListResult<EmployeeNotificationReadsDto>
        {
            Data = employeeNotificationReads,
            Count = employeeNotificationReads.Count
        };
    }
}