namespace Engage.Application.Services.EmployeeBenefits.Commands;

public class UpdateEmployeeBenefitCommand : EmployeeBenefitCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeBenefitCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeBenefitCommand, OperationStatus>
{
    public UpdateEmployeeBenefitCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeBenefitCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeBenefits.SingleAsync(x => x.EmployeeBenefitId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeBenefitId;
        return opStatus;
    }
}
