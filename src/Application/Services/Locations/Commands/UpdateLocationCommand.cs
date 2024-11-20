namespace Engage.Application.Services.Locations.Commands;

public class UpdateLocationCommand : LocationCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateLocationCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateLocationCommand, OperationStatus>
{
    public UpdateLocationCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Locations.SingleAsync(x => x.LocationId == command.Id, cancellationToken);
        if (command.Lat.HasValue)
        {
            //entity.Lat = command.Lat.Value > 0 ? float.Parse("-" + command.Lat.Value.ToString()) : command.Lat.Value;
            command.Lat = command.Lat.Value > 0 ? -command.Lat.Value : command.Lat.Value;
        }

        return await SaveChangesAsync(command, entity, nameof(Location), command.Id, cancellationToken);
    }
}
