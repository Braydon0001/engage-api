namespace Engage.Application.Services.EngageVariantProducts.Commands
{
    public class UpdateEngageVariantProductCommand : EngageVariantProductCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateEngageVariantProductCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageVariantProductCommand, OperationStatus>
    {
        public UpdateEngageVariantProductCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(UpdateEngageVariantProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.EngageVariantProducts
                .FirstOrDefaultAsync(x => x.EngageVariantProductId == command.Id);

            if (entity == null)
                throw new NotFoundException(nameof(EngageVariantProduct), command.Id);

            _mapper.Map(command, entity);

            if (entity.IsMaster)
            {
                var masterVariant = await _context.EngageMasterProducts.FirstOrDefaultAsync(e => e.EngageMasterProductId == entity.EngageMasterProductId);

                masterVariant.Code = entity.Code;
                masterVariant.Name = entity.Name;
            }

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EngageVariantProductId;

            return opStatus;
        }
    }
}
