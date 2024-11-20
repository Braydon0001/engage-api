using Engage.Application.Services.Stakeholders;

namespace Engage.Application.Services.Locations.Commands;

public class CreateLocationCommand : LocationCommand, IRequest<OperationStatus>
{
    public StakeholderTypes StakeholderType { get; set; }
    public int EntityId { get; set; }
}

public class CreateLocationCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateLocationCommand, OperationStatus>
{
    public CreateLocationCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
        : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateLocationCommand, Location>(command);
        if (command.Lat.HasValue)
        {
            //entity.Lat = command.Lat.Value > 0 ? float.Parse("-" + command.Lat.Value.ToString()) : command.Lat.Value;
            entity.Lat = command.Lat.Value > 0 ? -command.Lat.Value : command.Lat.Value;
        }

        entity.StakeholderId = await StakeholderUtils.GetIdForType(_mediator, command.StakeholderType, command.EntityId);
        _context.Locations.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.LocationId;
        return opStatus;
    }
}
