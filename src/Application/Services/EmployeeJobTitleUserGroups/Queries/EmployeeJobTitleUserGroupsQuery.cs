using Engage.Application.Services.EmployeeJobTitleUserGroups.Models;

namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Queries;

public class EmployeeJobTitleUserGroupsQuery : GetQuery, IRequest<ListResult<EmployeeJobTitleUserGroupDto>>
{
    public int? EmployeeJobTitleId { get; set; }
    public int? UserGroupId { get; set; }
}

public class EmployeeJobTitleUserGroupQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitleUserGroupsQuery, ListResult<EmployeeJobTitleUserGroupDto>>
{
    public EmployeeJobTitleUserGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeJobTitleUserGroupDto>> Handle(EmployeeJobTitleUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeJobTitleUserGroups.AsQueryable();

        if (request.EmployeeJobTitleId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeJobTitleId == request.EmployeeJobTitleId);
        }

        if (request.UserGroupId.HasValue)
        {
            queryable = queryable.Where(e => e.UserGroupId == request.UserGroupId);
        }

        var entities = await queryable.OrderBy(e => e.EmployeeJobTitleUserGroupId)
                                      .ProjectTo<EmployeeJobTitleUserGroupDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeJobTitleUserGroupDto>(entities);
    }
}
