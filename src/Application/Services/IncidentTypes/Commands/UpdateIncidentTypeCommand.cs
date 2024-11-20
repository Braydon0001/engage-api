namespace Engage.Application.Services.IncidentTypes.Commands;

public class UpdateIncidentTypeCommand : IncidentTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateIncidentTypeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateIncidentTypeCommand, OperationStatus>
{
    public UpdateIncidentTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateIncidentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentTypes.SingleAsync(x => x.IncidentTypeId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
