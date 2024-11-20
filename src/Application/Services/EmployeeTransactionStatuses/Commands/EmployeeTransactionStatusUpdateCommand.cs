namespace Engage.Application.Services.EmployeeTransactionStatuses.Commands;

public class EmployeeTransactionStatusUpdateCommand : IMapTo<EmployeeTransactionStatus>, IRequest<EmployeeTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void MapTo(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionStatusUpdateCommand, EmployeeTransactionStatus>();
    }
}

public class EmployeeTransactionStatusUpdateCommandHandler : UpdateHandler, IRequestHandler<EmployeeTransactionStatusUpdateCommand, EmployeeTransactionStatus>
{
    public EmployeeTransactionStatusUpdateCommandHandler (IAppDbContext context, IMapper mapper): base(context,mapper) { }

    public async Task<EmployeeTransactionStatus> Handle(EmployeeTransactionStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTransactionStatuses.SingleOrDefaultAsync(e => e.EmployeeTransactionStatusId == command.Id, cancellationToken);
        if (entity == null) { 
            return null; 
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeTransactionStatusValidator: AbstractValidator<EmployeeTransactionStatusUpdateCommand>
{
    public UpdateEmployeeTransactionStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name);
    }
}