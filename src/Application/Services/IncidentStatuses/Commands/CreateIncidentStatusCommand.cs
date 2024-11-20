namespace Engage.Application.Services.IncidentStatuses.Commands;

public class CreateIncidentStatusCommand : IncidentStatusCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentStatusCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentStatusCommand, OperationStatus>
{
    public CreateIncidentStatusCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentStatusCommand, IncidentStatus>(request);
        _context.IncidentStatuses.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentStatusId;
        return opStatus;
    }
}
