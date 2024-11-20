namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;

public class UpdateEmployeeJobTitleUserGroupCommand : EmployeeJobTitleUserGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeJobTitleUserGroupCommandHandler : BaseCreateCommandHandler, IRequestHandler<UpdateEmployeeJobTitleUserGroupCommand, OperationStatus>
{
    public UpdateEmployeeJobTitleUserGroupCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeJobTitleUserGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeJobTitleUserGroups.SingleAsync(x => x.EmployeeJobTitleUserGroupId == request.Id, cancellationToken);

        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeJobTitleUserGroupId;
        return opStatus;
    }
}
