namespace Engage.Application.Services.IncidentSkus.Commands;

public class CreateIncidentSkuCommand : IncidentSkuCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentSkuCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentSkuCommand, OperationStatus>
{
    public CreateIncidentSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentSkuCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentSkuCommand, IncidentSku>(request);
        _context.IncidentSkus.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentSkuId;
        return opStatus;
    }
}
