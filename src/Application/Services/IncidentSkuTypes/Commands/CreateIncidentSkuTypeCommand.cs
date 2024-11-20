namespace Engage.Application.Services.IncidentSkuTypes.Commands;

public class CreateIncidentSkuTypeCommand : IncidentSkuTypeCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentSkuTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentSkuTypeCommand, OperationStatus>
{
    public CreateIncidentSkuTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentSkuTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentSkuTypeCommand, IncidentSkuType>(request);
        _context.IncidentSkuTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentSkuTypeId;
        return opStatus;
    }
}
