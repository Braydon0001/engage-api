namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelCreateCommand : EmployeeFuelCommand, IRequest<OperationStatus>
{

}

public class EmployeeFuelCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeFuelCreateCommand, OperationStatus>
{

    public EmployeeFuelCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeFuelCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeFuelCreateCommand, EmployeeFuel>(command);
        _context.EmployeeFuels.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeFuelId;
        return opStatus;
    }
}
