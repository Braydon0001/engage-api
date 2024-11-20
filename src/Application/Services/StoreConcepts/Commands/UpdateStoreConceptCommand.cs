namespace Engage.Application.Services.StoreConcepts.Commands;

public class UpdateStoreConceptCommand : StoreConceptCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateUpdateStoreConceptCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStoreConceptCommand, OperationStatus>
{
    public UpdateUpdateStoreConceptCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateStoreConceptCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConcepts.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;

        return opStatus;
    }
}
