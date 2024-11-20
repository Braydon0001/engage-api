namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelUpdateCommand : EmployeeFuelCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeFuelUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeFuelUpdateCommand, OperationStatus>
{
    public EmployeeFuelUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeFuelUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFuels.SingleAsync(x => x.EmployeeFuelId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeFuelId;
        return opStatus;
    }
}
