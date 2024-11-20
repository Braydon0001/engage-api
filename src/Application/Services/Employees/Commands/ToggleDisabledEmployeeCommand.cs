using Engage.Application.Services.Users.Commands;

namespace Engage.Application.Services.Employees.Commands;

public class ToggleDisabledEmployeeCommand: IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class ToggleDisabledEmployeeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<ToggleDisabledEmployeeCommand, OperationStatus>
{
    public ToggleDisabledEmployeeCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
        base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(ToggleDisabledEmployeeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
                                .Where(e => e.EmployeeId == command.Id)
                                .FirstOrDefaultAsync(cancellationToken);

        if (entity != null)
        {
            entity.Disabled = !entity.Disabled;
            if (entity.UserId.HasValue)
            {
                await _mediator.Send(new RemoveUserCommand { Id = (int)entity.UserId });
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
