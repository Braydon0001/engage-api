namespace Engage.Application.Services.EmployeeWorkRoles.Commands;

public class UpdateEmployeeWorkRoleCommand : EmployeeWorkRoleCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeWorkRoleCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeWorkRoleCommand, OperationStatus>
{
    public UpdateEmployeeWorkRoleCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeWorkRoleCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeWorkRoles.SingleAsync(x => x.EmployeeWorkRoleId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.IsCurrentRole)
        {
            var existingCurrentRole = await _context.EmployeeWorkRoles
                                                    .Where(e => e.EmployeeWorkRoleId != command.Id
                                                        && e.IsCurrentRole == true
                                                        && e.Disabled == false)
                                                    .FirstOrDefaultAsync();
            if (existingCurrentRole != null)
            {
                existingCurrentRole.IsCurrentRole = false;
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeWorkRoleId;
        return opStatus;
    }
}
