namespace Engage.Application.Services.EmployeeTransactionStatuses.Commands;

public class EmployeeTransactionStatusInsertCommand : IMapTo<EmployeeTransactionStatus>, IRequest<EmployeeTransactionStatus>
{
    public string Name { get; set; }
    public void MapTo(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionStatusInsertCommand, EmployeeTransactionStatus>();
    }
}

public class EmployeeTransactionStatusInsertCommandHandler : InsertHandler, IRequestHandler<EmployeeTransactionStatusInsertCommand, EmployeeTransactionStatus>
{
    public EmployeeTransactionStatusInsertCommandHandler (IAppDbContext context, IMapper mapper): base (context, mapper)
    {
    }

    public async Task<EmployeeTransactionStatus> Handle(EmployeeTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeTransactionStatusInsertCommand, EmployeeTransactionStatus>(command);
        _context.EmployeeTransactionStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeTransactionStatusInsertValidator : AbstractValidator<EmployeeTransactionStatusInsertCommand>
{
    public EmployeeTransactionStatusInsertValidator()
    {
        RuleFor(e => e.Name);
    }
}