namespace Engage.Application.Services.StoreConcepts.Commands
{
    public class CreateStoreConceptCommand : StoreConceptCommand, IRequest<OperationStatus>
    {
    }

    public class CreateStoreConceptCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStoreConceptCommand, OperationStatus>
    {
        public CreateStoreConceptCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(CreateStoreConceptCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateStoreConceptCommand, StoreConcept>(command);
            _context.StoreConcepts.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.Id;

            return opStatus;
        }
    }
}
