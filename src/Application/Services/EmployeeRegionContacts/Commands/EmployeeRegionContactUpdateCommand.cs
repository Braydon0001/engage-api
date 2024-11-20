namespace Engage.Application.Services.EmployeeRegionContacts.Commands;

public class EmployeeRegionContactUpdateCommand : EmployeeRegionContactCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeRegionContactUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeRegionContactUpdateCommand, OperationStatus>
{
    public EmployeeRegionContactUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(EmployeeRegionContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeRegionContacts.SingleAsync(x => x.EmployeeRegionContactId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeRegionContactId;
        return opStatus;
    }
}
