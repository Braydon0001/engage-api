namespace Engage.Application.Services.Incidents.Commands;

public class UpdateIncidentCommand : IncidentCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentCommand, OperationStatus>
{
    public UpdateIncidentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Incidents.SingleAsync(x => x.IncidentId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
