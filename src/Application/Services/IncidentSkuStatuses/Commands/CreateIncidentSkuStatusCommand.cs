namespace Engage.Application.Services.IncidentSkuStatuses.Commands;

public class CreateIncidentSkuStatusCommand : IncidentSkuStatusCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentSkuStatusCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentSkuStatusCommand, OperationStatus>
{
    public CreateIncidentSkuStatusCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentSkuStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentSkuStatusCommand, IncidentSkuStatus>(request);
        _context.IncidentSkuStatuses.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentSkuStatusId;
        return opStatus;
    }
}
