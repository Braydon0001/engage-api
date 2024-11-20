namespace Engage.Application.Services.EmployeeRegionContacts.Commands;

public class EmployeeRegionContactCreateCommand : EmployeeRegionContactCommand, IRequest<OperationStatus>
{
    public int EngageRegionId { get; set; }
}

public class EmployeeRegionContactCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeRegionContactCreateCommand, OperationStatus>
{
    public EmployeeRegionContactCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeRegionContactCreateCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<EmployeeRegionContactCreateCommand, EmployeeRegionContact>(command);
        entity.EngageRegionId = command.EngageRegionId;
        _context.EmployeeRegionContacts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeRegionContactId;
        return opStatus;
    }
}
