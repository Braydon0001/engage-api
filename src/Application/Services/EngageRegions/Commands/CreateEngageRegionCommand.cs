namespace Engage.Application.Services.EngageRegions.Commands;

public class CreateEngageRegionCommand : EngageRegionCommand, IRequest<OperationStatus>
{
}

public class CreateEngageRegionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageRegionCommand, OperationStatus>
{
    public CreateEngageRegionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageRegionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageRegionCommand, EngageRegion>(command);
        _context.EngageRegions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
