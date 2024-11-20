namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeExemptionsCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeExemptionsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeExemptionsCommand, OperationStatus>
{
    public UpdateEmployeeExemptionsCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeExemptionsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;

    }
}

public class UpdateEmployeeExemptionsValidator : AbstractValidator<UpdateEmployeeExemptionsCommand>
{
    public UpdateEmployeeExemptionsValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
