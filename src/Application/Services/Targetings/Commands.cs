namespace Engage.Application.Services.Targetings;

// Commands
public class TargetingCommand : IRequest<OperationStatus>, IMapTo<Targeting>
{
    public string Criteria { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TargetingCommand, Targeting>();
    }
}

public class CreateTargetingCommand : TargetingCommand, IRequest<OperationStatus>
{

}

public class UpdateTargetingCommand : TargetingCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

// Handlers
public class CreateTargetingCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTargetingCommand, OperationStatus>
{
    public CreateTargetingCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateTargetingCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Targeting>(command);
        _context.Targetings.Add(entity);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class UpdateTargetingCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTargetingCommand, OperationStatus>
{
    public UpdateTargetingCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
    public async Task<OperationStatus> Handle(UpdateTargetingCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Targetings.FindAsync(command.Id, cancellationToken);
        return await SaveChangesAsync(command, entity, nameof(Targeting), command.Id, cancellationToken);
    }
}
