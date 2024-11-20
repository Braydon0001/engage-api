namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Commands;

public class EmployeeRecurringTransactionStatusUpdateCommand : IMapTo<EmployeeRecurringTransactionStatus>, IRequest<EmployeeRecurringTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void MapTo(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionStatusUpdateCommand, EmployeeRecurringTransactionStatus>();
    }
}

public class EmployeeRecurringTransactionStatusUpdateHandler : InsertHandler, IRequestHandler<EmployeeRecurringTransactionStatusUpdateCommand, EmployeeRecurringTransactionStatus>
{
    public EmployeeRecurringTransactionStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) 
    {
    }

    public async Task<EmployeeRecurringTransactionStatus> Handle(EmployeeRecurringTransactionStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeRecurringTransactionStatuses.SingleOrDefaultAsync(e => e.EmployeeRecurringTransactionStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeRecurringTransactionStatusValidator : AbstractValidator<EmployeeRecurringTransactionStatusUpdateCommand>
{
    public UpdateEmployeeRecurringTransactionStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name);
    }
}