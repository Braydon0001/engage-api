namespace Engage.Application.Services.EmployeeEmployeeKpis.Commands;

public record EmployeeEmployeeKpiBulkUpdateCommand(List<EmployeeEmployeeKpiUpdateCommand> updates) : IRequest<OperationStatus>
{
}

public class EmployeeEmployeeKpiBulkUpdateHandler : IRequestHandler<EmployeeEmployeeKpiBulkUpdateCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public EmployeeEmployeeKpiBulkUpdateHandler(IMediator mediator, IAppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeEmployeeKpiBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.updates)
        {
            await _mediator.Send(new EmployeeEmployeeKpiUpdateCommand
            {
                EmployeeId = update.EmployeeId,
                EmployeeKpiId = update.EmployeeKpiId,
                Score = update.Score,
                SaveChanges = false
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus(true);
    }
}
