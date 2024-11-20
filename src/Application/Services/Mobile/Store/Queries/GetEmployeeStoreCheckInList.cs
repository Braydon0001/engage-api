using Engage.Application.Services.Mobile.Stores.Models;

namespace Engage.Application.Services.Mobile.Stores.Queries;

public class GetEmployeeStoreCheckInListQuery : GetQuery, IRequest<ListResult<EmployeeStoreCheckInListItemDto>>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeStoreCheckInListQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeStoreCheckInListQuery, ListResult<EmployeeStoreCheckInListItemDto>>
{
    public GetEmployeeStoreCheckInListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeStoreCheckInListItemDto>> Handle(GetEmployeeStoreCheckInListQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.EmployeeStoreCheckIns.Where(x => x.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(x => x.CheckInTimezoneDate).AsQueryable();

        if (request.Search != null)
        {
            entities = entities.Where(x => (EF.Functions.Like(x.Store.Name, $"%{request.Search}%")));
        };

        var data = await entities.ProjectTo<EmployeeStoreCheckInListItemDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreCheckInListItemDto>(data);
    }
}
