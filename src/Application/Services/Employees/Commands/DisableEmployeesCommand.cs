using Engage.Application.Services.Users.Commands;

namespace Engage.Application.Services.Employees.Commands;

public class DisableEmployeesCommand: IRequest<OperationStatus>
{
    public int[] EmployeeIDs { get; set; }
}

public class DisableEmployeesCommandHandler : BaseUpdateCommandHandler, IRequestHandler<DisableEmployeesCommand, OperationStatus>
{
    public DisableEmployeesCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
        base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(DisableEmployeesCommand command, CancellationToken cancellationToken)
    {
        var entities = await _context.Employees
                                .Where(e => command.EmployeeIDs.Contains(e.EmployeeId))
                                .ToListAsync(cancellationToken);

        if (entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.Disabled = true;

                if (entity.UserId.HasValue)
                {
                    await _mediator.Send(new RemoveUserCommand { Id = (int)entity.UserId });
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
