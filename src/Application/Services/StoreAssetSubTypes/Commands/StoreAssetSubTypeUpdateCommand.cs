// auto-generated
using Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;

namespace Engage.Application.Services.StoreAssetSubTypes.Commands;

public class StoreAssetSubTypeUpdateCommand : IMapTo<StoreAssetSubType>, IRequest<StoreAssetSubType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> StoreAssetTypeIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetSubTypeUpdateCommand, StoreAssetSubType>();
    }
}

public class StoreAssetSubTypeUpdateHandler : UpdateHandler, IRequestHandler<StoreAssetSubTypeUpdateCommand, StoreAssetSubType>
{
    private readonly IMediator _mediator;
    public StoreAssetSubTypeUpdateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<StoreAssetSubType> Handle(StoreAssetSubTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssetSubTypes.SingleOrDefaultAsync(e => e.StoreAssetSubTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _mediator.Send(new StoreAssetTypeStoreAssetSubTypeCreateCommand
        {
            StoreAssetSubTypeId = command.Id,
            StoreAssetTypeIds = command.StoreAssetTypeIds,
            Save = false
        });

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetSubTypeValidator : AbstractValidator<StoreAssetSubTypeUpdateCommand>
{
    public UpdateStoreAssetSubTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StoreAssetTypeIds);
    }
}