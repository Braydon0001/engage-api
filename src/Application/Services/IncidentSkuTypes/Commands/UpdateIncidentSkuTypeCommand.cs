namespace Engage.Application.Services.IncidentSkuTypes.Commands;

public class UpdateIncidentSkuTypeCommand : IncidentSkuTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentSkuTypeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentSkuTypeCommand, OperationStatus>
{
    public UpdateIncidentSkuTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentSkuTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkuTypes.SingleAsync(x => x.IncidentSkuTypeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
