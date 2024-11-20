namespace Engage.Application.Services.EmployeeTransactions.Commands;

public record EmployeeTransactionBulkUpdateCommand(List<EmployeeTransactionUpdateCommand> Updates) : IRequest<OperationStatus>
{

}

public class EmployeeTransactionBulkUpdateHandler : IRequestHandler<EmployeeTransactionBulkUpdateCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public EmployeeTransactionBulkUpdateHandler(IMediator mediator, IAppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeTransactionBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.Updates)
        {
            await _mediator.Send(new EmployeeTransactionUpdateCommand
            {
                Id = update.Id,
                //EmployeeId = update.EmployeeId,
                //EmployeeTransactionTypeId = update.EmployeeTransactionTypeId,
                Amount = update.Amount,
                Rate = update.Rate,
                Days = update.Days,
                Hours = update.Hours,
                UnpaidDays = update.UnpaidDays,
                UnpaidHours = update.UnpaidHours,
                Note = update.Note,
                ////recurring
                LeavePayPercentage = update.LeavePayPercentage,
                //CreditorBankAccountId = update.CreditorBankAccountId,
                StartDate = update.StartDate,
                EndDate = update.EndDate,
                InitialAmount = update.InitialAmount,
                InstallmentAmount = update.InstallmentAmount,
                BaseInstallmentOnAmountOrComponent = update.BaseInstallmentOnAmountOrComponent,
                Reference = update.Reference,
                //IsFringeBenefitLoan = update.IsFringeBenefitLoan,
                SaveChanges = false
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus(true);
    }
}