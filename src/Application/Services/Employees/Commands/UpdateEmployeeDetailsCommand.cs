namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeDetailsCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeDetailsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeDetailsCommand, OperationStatus>
{
    public UpdateEmployeeDetailsCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeDetailsCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}

public class UpdateEmployeeDetailsValidator : AbstractValidator<UpdateEmployeeDetailsCommand>
{
    public UpdateEmployeeDetailsValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
