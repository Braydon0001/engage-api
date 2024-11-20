namespace Engage.Application.Services.EmployeeWorkRoles.Commands;

public class CreateEmployeeWorkRoleCommand : EmployeeWorkRoleCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeWorkRoleCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeWorkRoleCommand, OperationStatus>
{
    public CreateEmployeeWorkRoleCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeWorkRoleCommand command, CancellationToken cancellationToken)
    {
        if (command.IsCurrentRole)
        {
            var existingCurrentRole = await _context.EmployeeWorkRoles
                                                    .Where(e => e.EmployeeId == command.EmployeeId
                                                        && e.IsCurrentRole == true
                                                        && e.Disabled == false)
                                                    .FirstOrDefaultAsync();
            if (existingCurrentRole != null)
            {
                existingCurrentRole.IsCurrentRole = false;
            }
        }

        var entity = _mapper.Map<CreateEmployeeWorkRoleCommand, EmployeeWorkRole>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeWorkRoles.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeWorkRoleId;
        return opStatus;
    }
}
