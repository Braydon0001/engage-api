// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactions.Commands;

public class EmployeeRecurringTransactionInsertCommand : IMapTo<EmployeeRecurringTransaction>, IRequest<EmployeeRecurringTransaction>
{
    public int EmployeeId { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public int EmployeeRecurringTransactionStatusId { get; set; }
    public int PayrollPeriodId { get; set; }
    public int? CreditorBankAccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public string BaseInstallmentOnAmountOrComponent { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; }
    public float LeavePayPercentage { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionInsertCommand, EmployeeRecurringTransaction>();
    }
}

public class EmployeeRecurringTransactionInsertHandler : InsertHandler, IRequestHandler<EmployeeRecurringTransactionInsertCommand, EmployeeRecurringTransaction>
{
    public EmployeeRecurringTransactionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeRecurringTransaction> Handle(EmployeeRecurringTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeRecurringTransactionInsertCommand, EmployeeRecurringTransaction>(command);
        
        _context.EmployeeRecurringTransactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeRecurringTransactionInsertValidator : AbstractValidator<EmployeeRecurringTransactionInsertCommand>
{
    public EmployeeRecurringTransactionInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeTransactionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeRecurringTransactionStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PayrollPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorBankAccountId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.InitialAmount);
        RuleFor(e => e.InstallmentAmount);
        RuleFor(e => e.BaseInstallmentOnAmountOrComponent).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(220);
        RuleFor(e => e.Reference).MaximumLength(220);
        RuleFor(e => e.IsFringeBenefitLoan);
        RuleFor(e => e.LeavePayPercentage);
    }
}