namespace Engage.Application.Services.EmployeeStoreKpiScores.Commands;

public record EmployeeStoreKpiScoreBulkUpdateCommand(List<EmployeeStoreKpiScoreUpdateCommand> updates) : IRequest<OperationStatus>
{
}

public class EmployeeStoreKpiScoreBulkUpdateHandler : IRequestHandler<EmployeeStoreKpiScoreBulkUpdateCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public EmployeeStoreKpiScoreBulkUpdateHandler(IMediator mediator, IAppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreKpiScoreBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.updates)
        {
            await _mediator.Send(new EmployeeStoreKpiScoreUpdateCommand
            {
                Id = update.Id,
                EmployeeId = update.EmployeeId,
                StoreId = update.StoreId,
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
