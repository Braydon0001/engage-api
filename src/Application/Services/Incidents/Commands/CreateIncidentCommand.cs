namespace Engage.Application.Services.Incidents.Commands;

public class CreateIncidentCommand : IncidentCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentCommand, OperationStatus>
{
    public CreateIncidentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentCommand, Incident>(request);
        _context.Incidents.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentId;
        return opStatus;
    }
}
