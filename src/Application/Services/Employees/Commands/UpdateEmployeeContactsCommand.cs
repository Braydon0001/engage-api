namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeContactsCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeContactsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeContactsCommand, OperationStatus>
{
    public UpdateEmployeeContactsCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeContactsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}

public class UpdateEmployeeContactsValidator : AbstractValidator<UpdateEmployeeContactsCommand>
{
    public UpdateEmployeeContactsValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
