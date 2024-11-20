namespace Engage.Application.Services.UserUserGroups.Commands;

public class UserUserGroupCreateCommand : UserUserGroupCommand, IRequest<OperationStatus>
{
}
public class UserUserGroupCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<UserUserGroupCreateCommand, OperationStatus>
{
    public UserUserGroupCreateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UserUserGroupCreateCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<UserUserGroupCreateCommand, UserUserGroup>(request);
        _context.UserUserGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.UserUserGroupId;
        return opStatus;
    }
}

