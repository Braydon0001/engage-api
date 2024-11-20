using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionInsertCommand : IMapTo<EmployeeTransaction>, IRequest<EmployeeTransaction>
{
    public int EmployeeId { get; set; }
    public int EmployeeTransactionTypeId { get; set; }
    public int? EmployeeTransactionRemunerationTypeId { get; set; }
    public decimal? Amount { get; set; } = 0;
    public decimal? Rate { get; set; } = 0;
    public float? Days { get; set; } = 0;
    public float? Hours { get; set; } = 0;
    public float? UnpaidDays { get; set; } = 0;
    public float? UnpaidHours { get; set; } = 0;
    public string Note { get; set; }
    //recurring
    public float? LeavePayPercentage { get; set; } = 0;
    public int? CreditorBankAccountId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? InitialAmount { get; set; } = 0;
    public decimal? InstallmentAmount { get; set; } = 0;
    public string BaseInstallmentOnAmountOrComponent { get; set; } = "Amount";
    public string Reference { get; set; }
    public bool IsFringeBenefitLoan { get; set; } = false;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionInsertCommand, EmployeeTransaction>();
    }
}

public class EmployeeTransactionInsertHandler : InsertHandler, IRequestHandler<EmployeeTransactionInsertCommand, EmployeeTransaction>
{
    private readonly IMediator _mediator;
    public EmployeeTransactionInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<EmployeeTransaction> Handle(EmployeeTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var payrollPeriod = await _context.PayrollPeriods.SingleOrDefaultAsync(e
            => DateTime.Now.Date >= e.StartDate.Date && DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (payrollPeriod == null)
        {
            throw new Exception("No Payroll Period found");
        }

        var transactionType = await _context.EmployeeTransactionTypes.SingleOrDefaultAsync(e => e.EmployeeTransactionTypeId == command.EmployeeTransactionTypeId, cancellationToken);
        if (transactionType == null)
        {
            throw new Exception("No Transaction Type found");
        }

        var entity = _mapper.Map<EmployeeTransactionInsertCommand, EmployeeTransaction>(command);

        entity.PayrollPeriodId = payrollPeriod.PayrollPeriodId;
        entity.EmployeeTransactionStatusId = (int)EmployeeTransactionStatusId.Captured;

        //check if transaction type is recurring
        if (transactionType.IsRecurring)
        {
            var recurringTransaction = new EmployeeRecurringTransaction
            {
                EmployeeId = command.EmployeeId,
                EmployeeTransactionTypeId = command.EmployeeTransactionTypeId,
                EmployeeRecurringTransactionStatusId = (int)EmployeeRecurringTransactionStatusId.Captured,
                CreditorBankAccountId = command.CreditorBankAccountId,
                StartDate = command.StartDate ?? DateTime.Now,
                EndDate = command.EndDate,
                InitialAmount = command.InitialAmount ?? 0,
                InstallmentAmount = command.InstallmentAmount ?? 0,
                Note = command.Note,
                LeavePayPercentage = command.LeavePayPercentage ?? 0,
                PayrollPeriodId = payrollPeriod.PayrollPeriodId,
                BaseInstallmentOnAmountOrComponent = command.BaseInstallmentOnAmountOrComponent,
                Reference = command.Reference,
                IsFringeBenefitLoan = command.IsFringeBenefitLoan,
            };

            entity.EmployeeRecurringTransaction = recurringTransaction;
        }

        _context.EmployeeTransactions.Add(entity);

        var response = await _context.SaveChangesAsync(cancellationToken);

        if (response.Status)
        {
            var template = await _context.CommunicationTemplates
                                              .Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.EmployeeTransaction)
                                              .FirstOrDefaultAsync(cancellationToken);

            var employee = await _context.Employees.Include(e => e.Manager)
                                                   .SingleOrDefaultAsync(e => e.EmployeeId == command.EmployeeId, cancellationToken);

            if (template != null && employee != null)
            {
                await _mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = employee.Manager.EmailAddress1,
                    FromEmailAddress = template.FromEmailAddress,
                    FromEmailName = template.FromName,
                    Subject = template.Subject,
                    Body = template.Body,
                    TemplateData = new
                    {
                        ManagerName = employee.Manager.FirstName,
                        EmployeeName = $"{employee.FirstName} {employee.LastName} - {employee.Code}",
                        UserEmail = entity.CreatedBy
                    },
                }, cancellationToken);
            }
        }

        return entity;
    }
}

public class EmployeeTransactionInsertValidator : AbstractValidator<EmployeeTransactionInsertCommand>
{
    public EmployeeTransactionInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeTransactionTypeId).NotEmpty().GreaterThan(0);
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
        RuleFor(e => e.CreditorBankAccountId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.InitialAmount);
        RuleFor(e => e.InstallmentAmount);
        RuleFor(e => e.BaseInstallmentOnAmountOrComponent);
        RuleFor(e => e.Reference);
        RuleFor(e => e.IsFringeBenefitLoan);
    }
}