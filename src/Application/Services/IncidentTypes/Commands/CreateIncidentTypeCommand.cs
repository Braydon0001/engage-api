namespace Engage.Application.Services.IncidentTypes.Commands;

public class CreateIncidentTypeCommand : IncidentTypeCommand, IRequest<OperationStatus>
{
}

public class CreateIncidentTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateIncidentTypeCommand, OperationStatus>
{
    public CreateIncidentTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateIncidentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateIncidentTypeCommand, IncidentType>(request);
        _context.IncidentTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.IncidentTypeId;
        return opStatus;
    }
}
