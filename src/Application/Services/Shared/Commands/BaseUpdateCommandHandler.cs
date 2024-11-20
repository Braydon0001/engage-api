namespace Engage.Application.Services.Shared.Commands;

public abstract class BaseUpdateCommandHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;

    protected BaseUpdateCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected BaseUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    protected async Task<OperationStatus> SaveChangesAsync<TCommand, TEntity>(TCommand command, TEntity entity, string entityName, int entityId, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            throw new NotFoundException(entityName, entityId);
        }

        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entityId;
        return opStatus;
    }

    protected async Task<OperationStatus> SaveChangesAsync(int id, CancellationToken cancellationToken)
    {
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = id;
        return opStatus;
    }
}
