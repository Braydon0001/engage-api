namespace Engage.Application.Services.UserUserGroups.Commands;

public class UserUserGroupUpdateCommand : UserUserGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}
public class UserUserGroupUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UserUserGroupUpdateCommand, OperationStatus>
{
    public UserUserGroupUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UserUserGroupUpdateCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.UserUserGroups.IgnoreQueryFilters().SingleAsync(x => x.UserUserGroupId == request.Id && !x.Deleted && !x.Disabled, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}

