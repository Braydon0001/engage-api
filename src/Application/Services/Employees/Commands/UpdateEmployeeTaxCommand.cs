namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeTaxCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeTaxCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeTaxCommand, OperationStatus>
{
    public UpdateEmployeeTaxCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeTaxCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;

    }
}

public class UpdateEmployeeTaxValidator : AbstractValidator<UpdateEmployeeTaxCommand>
{
    public UpdateEmployeeTaxValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
