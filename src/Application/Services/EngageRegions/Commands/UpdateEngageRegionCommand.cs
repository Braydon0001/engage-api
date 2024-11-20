namespace Engage.Application.Services.EngageRegions.Commands;

public class UpdateEngageRegionCommand : EngageRegionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEngageRegionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageRegionCommand, OperationStatus>
{
    public UpdateEngageRegionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageRegionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageRegions.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
