namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;

public class BatchCreateEmployeeJobTitleUserGroupsCommand : IRequest<OperationStatus>
{
    public List<int> EmployeeJobTitleIds { get; set; }
    public List<int> UserGroupIds { get; set; }
}

public class BatchCreateEmployeeJobTitleUserGroupsCommandHandler : BaseCreateCommandHandler, IRequestHandler<BatchCreateEmployeeJobTitleUserGroupsCommand, OperationStatus>
{
    public BatchCreateEmployeeJobTitleUserGroupsCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(BatchCreateEmployeeJobTitleUserGroupsCommand request, CancellationToken cancellationToken)
    {
        foreach (var jobTitleId in request.EmployeeJobTitleIds)
        {
            foreach (var groupId in request.UserGroupIds)
            {
                var exists = await _context.EmployeeJobTitleUserGroups.AnyAsync(x => x.EmployeeJobTitleId == jobTitleId && x.UserGroupId == groupId,
                                                                                cancellationToken);
                if (!exists)
                {
                    var entity = new EmployeeJobTitleUserGroup
                    {
                        EmployeeJobTitleId = jobTitleId,
                        UserGroupId = groupId,
                    };

                    _context.EmployeeJobTitleUserGroups.Add(entity);
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
