namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;

public class DeleteEmployeeJobTitleUserGroupCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteEmployeeJobTitleUserGroupCommandHandler : IRequestHandler<DeleteEmployeeJobTitleUserGroupCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeleteEmployeeJobTitleUserGroupCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeleteEmployeeJobTitleUserGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeJobTitleUserGroups.SingleAsync(s => s.EmployeeJobTitleUserGroupId == request.Id, cancellationToken);

        _context.EmployeeJobTitleUserGroups.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
