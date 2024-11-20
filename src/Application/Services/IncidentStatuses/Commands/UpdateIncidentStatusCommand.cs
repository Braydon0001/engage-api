namespace Engage.Application.Services.IncidentStatuses.Commands;

public class UpdateIncidentStatusCommand : IncidentStatusCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentStatusCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentStatusCommand, OperationStatus>
{
    public UpdateIncidentStatusCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentStatuses.SingleAsync(x => x.IncidentStatusId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
