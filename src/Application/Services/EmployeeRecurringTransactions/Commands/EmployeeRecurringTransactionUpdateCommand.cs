// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactions.Commands;

public class EmployeeRecurringTransactionUpdateCommand : IMapTo<EmployeeRecurringTransaction>, IRequest<EmployeeRecurringTransaction>
{
    public int Id { get; set; }
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
        profile.CreateMap<EmployeeRecurringTransactionUpdateCommand, EmployeeRecurringTransaction>();
    }
}

public class EmployeeRecurringTransactionUpdateHandler : UpdateHandler, IRequestHandler<EmployeeRecurringTransactionUpdateCommand, EmployeeRecurringTransaction>
{
    public EmployeeRecurringTransactionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeRecurringTransaction> Handle(EmployeeRecurringTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeRecurringTransactions.SingleOrDefaultAsync(e => e.EmployeeRecurringTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeRecurringTransactionValidator : AbstractValidator<EmployeeRecurringTransactionUpdateCommand>
{
    public UpdateEmployeeRecurringTransactionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
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