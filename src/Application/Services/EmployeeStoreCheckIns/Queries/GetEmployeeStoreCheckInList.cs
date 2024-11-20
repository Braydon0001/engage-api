using Engage.Application.Services.EmployeeStoreCheckIns.Models;

namespace Engage.Application.Services.EmployeeStoreCheckIns.Queries;

public class GetEmployeeStoreCheckInListQuery : GetQuery, IRequest<ListResult<EmployeeStoreCheckInListItemDto>>
{
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
}

public class GetEmployeeStoreCheckInListQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeStoreCheckInListQuery, ListResult<EmployeeStoreCheckInListItemDto>>
{
    public GetEmployeeStoreCheckInListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeStoreCheckInListItemDto>> Handle(GetEmployeeStoreCheckInListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeStoreCheckIns.Where(x => x.EmployeeId == (request.EmployeeId ?? x.EmployeeId) &&
                                                                       x.StoreId == (request.StoreId ?? x.StoreId))
                                                           .OrderByDescending(x => x.CheckInTimezoneDate)
                                                           .ProjectTo<EmployeeStoreCheckInListItemDto>(_mapper.ConfigurationProvider)
                                                           //.Take(30) //Take the last 30 records
                                                           .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreCheckInListItemDto>(entities);
    }
}
