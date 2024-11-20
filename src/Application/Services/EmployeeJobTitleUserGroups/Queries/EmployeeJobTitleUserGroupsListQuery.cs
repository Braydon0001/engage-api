namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;

public class EmployeeJobTitleUserGroupsListQuery : IRequest<ListResult<OptionDto>>
{
    public List<int> EmployeeJobTitleIds { get; set; }
}

public class EmployeeJobTitleUserGroupsListQueryHandler(IAppDbContext context, IMapper mapper) : ListQueryHandler(context, mapper), IRequestHandler<EmployeeJobTitleUserGroupsListQuery, ListResult<OptionDto>>
{
    public async Task<ListResult<OptionDto>> Handle(EmployeeJobTitleUserGroupsListQuery request, CancellationToken cancellationToken)
    {
        var userGroups = await _context.EmployeeJobTitleUserGroups.Include(x => x.UserGroup)
                                                            .Where(x => request.EmployeeJobTitleIds.Contains(x.EmployeeJobTitleId))
                                                            .Select(x => new OptionDto
                                                            {
                                                                Id = x.UserGroupId,
                                                                Name = x.UserGroup.Name
                                                            }).ToListAsync(cancellationToken);

        return new ListResult<OptionDto>(userGroups);
    }
}
