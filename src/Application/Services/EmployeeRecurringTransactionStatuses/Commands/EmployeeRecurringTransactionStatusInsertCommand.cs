namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Commands;

public class EmployeeRecurringTransactionStatusInsertCommand : IMapTo<EmployeeRecurringTransactionStatus>, IRequest<EmployeeRecurringTransactionStatus>
{
    public String Name { get; set; }

    public void MapTo(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionStatusInsertCommand, EmployeeRecurringTransactionStatus>();
    }
}

public class EmployeeRecurringTransactionStatusInsertHandler: InsertHandler, IRequestHandler<EmployeeRecurringTransactionStatusInsertCommand,EmployeeRecurringTransactionStatus>
{
    public EmployeeRecurringTransactionStatusInsertHandler (IAppDbContext context, IMapper mapper) : base (context, mapper)
    {
    }

    public async Task<EmployeeRecurringTransactionStatus> Handle(EmployeeRecurringTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeRecurringTransactionStatusInsertCommand, EmployeeRecurringTransactionStatus>(command);
        _context.EmployeeRecurringTransactionStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeRecurringTransactionStatusInsertValidator : AbstractValidator<EmployeeRecurringTransactionStatusInsertCommand>
{
    public EmployeeRecurringTransactionStatusInsertValidator ()
    {
        RuleFor(e => e.Name);
    }
}