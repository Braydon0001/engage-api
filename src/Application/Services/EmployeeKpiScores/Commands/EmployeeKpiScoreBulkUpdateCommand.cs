namespace Engage.Application.Services.EmployeeKpiScores.Commands;

public record EmployeeKpiScoreBulkUpdateCommand(List<EmployeeKpiScoreUpdateCommand> updates) : IRequest<OperationStatus>
{
}

public class EmployeeKpiScoreBulkUpdateHandler : IRequestHandler<EmployeeKpiScoreBulkUpdateCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public EmployeeKpiScoreBulkUpdateHandler(IMediator mediator, IAppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeKpiScoreBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.updates)
        {
            await _mediator.Send(new EmployeeKpiScoreUpdateCommand
            {
                Id = update.Id,
                EmployeeId = update.EmployeeId,
                EmployeeKpiId = update.EmployeeKpiId,
                EmployeeKpiTierId = update.EmployeeKpiTierId,
                Score = update.Score,
                SaveChanges = false
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus(true);
    }
}
