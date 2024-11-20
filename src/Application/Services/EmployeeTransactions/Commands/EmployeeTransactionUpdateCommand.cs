namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionUpdateCommand : IMapTo<EmployeeTransaction>, IRequest<EmployeeTransaction>
{
    public int Id { get; set; }
    //public int EmployeeId { get; set; }
    //public int EmployeeTransactionTypeId { get; set; }
    //public DateTime? TransactionDate { get; set; }
    public decimal? Amount { get; set; } = 0;
    public decimal? Rate { get; set; } = 0;
    public float? Days { get; set; } = 0;
    public float? Hours { get; set; } = 0;
    public float? UnpaidDays { get; set; } = 0;
    public float? UnpaidHours { get; set; } = 0;
    public string Note { get; set; }

    //recurring
    public float? LeavePayPercentage { get; set; } = 0;
    //public int? CreditorBankAccountId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? InitialAmount { get; set; } = 0;
    public decimal? InstallmentAmount { get; set; } = 0;
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Reference { get; set; }
    //public bool IsFringeBenefitLoan { get; set; }
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionUpdateCommand, EmployeeTransaction>();
    }
}

public class EmployeeTransactionUpdateCommandHandler : UpdateHandler, IRequestHandler<EmployeeTransactionUpdateCommand, EmployeeTransaction>
{
    public EmployeeTransactionUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeTransaction> Handle(EmployeeTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTransactions.SingleOrDefaultAsync(e => e.EmployeeTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        var transactionType = await _context.EmployeeTransactionTypes.SingleOrDefaultAsync(e => e.EmployeeTransactionTypeId == entity.EmployeeTransactionTypeId, cancellationToken);
        if (transactionType == null)
        {
            throw new Exception("No Transaction Type found");
        }

        if (transactionType.IsRecurring)
        {
            var recurringTransaction = await _context.EmployeeRecurringTransactions.SingleOrDefaultAsync(e => e.EmployeeRecurringTransactionId == entity.EmployeeRecurringTransactionId, cancellationToken);
            if (recurringTransaction == null)
            {
                return null;
            }

            //recurringTransaction.CreditorBankAccountId = command.CreditorBankAccountId;
            recurringTransaction.StartDate = command.StartDate ?? DateTime.Now;
            recurringTransaction.EndDate = command.EndDate;
            recurringTransaction.InitialAmount = command.InitialAmount ?? 0;
            recurringTransaction.InstallmentAmount = command.InstallmentAmount ?? 0;
            recurringTransaction.Note = command.Note;
            recurringTransaction.LeavePayPercentage = command.LeavePayPercentage ?? 0;
            recurringTransaction.BaseInstallmentOnAmountOrComponent = command.BaseInstallmentOnAmountOrComponent;
            recurringTransaction.Reference = command.Reference;
            //recurringTransaction.IsFringeBenefitLoan = command.IsFringeBenefitLoan;
        }

        if (command.SaveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return mappedEntity;
    }
}

public class EmployeeTransactionUpdateCommandValidator : AbstractValidator<EmployeeTransactionUpdateCommand>
{
    public EmployeeTransactionUpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.EmployeeTransactionTypeId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.TransactionDate);
        RuleFor(e => e.Amount);
        RuleFor(e => e.Rate);
        RuleFor(e => e.Days);
        RuleFor(e => e.Hours);
        RuleFor(e => e.UnpaidDays);
        RuleFor(e => e.UnpaidHours);
        RuleFor(e => e.Note).MaximumLength(220);
        //recurring
        RuleFor(e => e.LeavePayPercentage);
        //RuleFor(e => e.CreditorBankAccountId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.InitialAmount);
        RuleFor(e => e.InstallmentAmount);
        RuleFor(e => e.BaseInstallmentOnAmountOrComponent);
        RuleFor(e => e.Reference);
        //RuleFor(e => e.IsFringeBenefitLoan);
    }
}