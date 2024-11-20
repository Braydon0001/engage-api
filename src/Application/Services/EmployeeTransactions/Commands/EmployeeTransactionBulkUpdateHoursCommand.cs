namespace Engage.Application.Services.EmployeeTransactions.Commands;


public record EmployeeHour(int Hours, string Note);

public record EmployeeTransactionBulkUpdateHoursCommand(Dictionary<string, EmployeeHour> Updates) : IRequest<OperationStatus>
{
}


public class EmployeeTransactionBulkUpdateHoursHandler : IRequestHandler<EmployeeTransactionBulkUpdateHoursCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeTransactionBulkUpdateHoursHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeTransactionBulkUpdateHoursCommand request, CancellationToken cancellationToken)
    {
        foreach (var update in request.Updates)
        {
            var id = update.Key;
            var hours = update.Value.Hours;
            var note = update.Value.Note;

            // SingleOrDefaultAsync  
            // Set Properties
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
