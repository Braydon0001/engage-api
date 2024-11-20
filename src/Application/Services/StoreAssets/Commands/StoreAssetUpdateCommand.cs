using Engage.Application.Services.StoreAssetStoreAssetTypeContact;

namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetUpdateCommand : StoreAssetCommand, IMapTo<StoreAsset>, IRequest<OperationStatus>
{
    public int Id { get; set; }

}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<StoreAssetUpdateCommand, OperationStatus>
{
    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(StoreAssetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssets.SingleAsync(x => x.StoreAssetId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        // store Concepts
        //if (command.StoreConceptAttributeIds != null)
        //{
        //    await _mediator.Send(new BatchAssignCommand(AssignDesc.STORE_ASSET_ATTRIBUTE, entity.StoreAssetId, command.StoreConceptAttributeIds), cancellationToken);

        //}

        if (command.StoreAssetTypeContactIds != null)
        {
            await _mediator.Send(new StoreAssetStoreAssetTypeContactInsertCommand
            {
                StoreAssetId = command.Id,
                StoreAssetTypeContactIds = command.StoreAssetTypeContactIds
            }, cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreAssetId;
        return opStatus;
    }
}

public class UpdateStoreAssetValidator : StoreAssetValidator<StoreAssetUpdateCommand>
{
    public UpdateStoreAssetValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}