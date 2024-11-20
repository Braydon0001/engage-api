namespace Engage.Application.Services.IncidentSkus.Commands;

public class UpdateIncidentSkuCommand : IncidentSkuCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentSkuCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentSkuCommand, OperationStatus>
{
    public UpdateIncidentSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentSkuCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkus.SingleAsync(x => x.IncidentSkuId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
