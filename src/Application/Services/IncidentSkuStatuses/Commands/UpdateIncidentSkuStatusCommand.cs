namespace Engage.Application.Services.IncidentSkuStatuses.Commands;

public class UpdateIncidentSkuStatusCommand : IncidentSkuStatusCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentSkuStatusCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentSkuStatusCommand, OperationStatus>
{
    public UpdateIncidentSkuStatusCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentSkuStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkuStatuses.SingleAsync(x => x.IncidentSkuStatusId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
