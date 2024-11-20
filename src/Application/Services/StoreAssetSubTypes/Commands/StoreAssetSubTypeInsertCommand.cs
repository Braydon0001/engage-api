// auto-generated
using Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;

namespace Engage.Application.Services.StoreAssetSubTypes.Commands;

public class StoreAssetSubTypeInsertCommand : IMapTo<StoreAssetSubType>, IRequest<StoreAssetSubType>
{
    public string Name { get; set; }
    public List<int> StoreAssetTypeIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetSubTypeInsertCommand, StoreAssetSubType>();
    }
}

public class StoreAssetSubTypeInsertHandler : InsertHandler, IRequestHandler<StoreAssetSubTypeInsertCommand, StoreAssetSubType>
{
    private readonly IMediator _mediator;
    public StoreAssetSubTypeInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<StoreAssetSubType> Handle(StoreAssetSubTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreAssetSubTypeInsertCommand, StoreAssetSubType>(command);

        _context.StoreAssetSubTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(new StoreAssetTypeStoreAssetSubTypeCreateCommand
        {
            StoreAssetSubTypeId = entity.StoreAssetSubTypeId,
            StoreAssetTypeIds = command.StoreAssetTypeIds
        }, cancellationToken);

        return entity;
    }
}

public class StoreAssetSubTypeInsertValidator : AbstractValidator<StoreAssetSubTypeInsertCommand>
{
    public StoreAssetSubTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StoreAssetTypeIds);
    }
}