// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Commands;

public class StoreAssetConditionUpdateCommand : IMapTo<StoreAssetCondition>, IRequest<StoreAssetCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetConditionUpdateCommand, StoreAssetCondition>();
    }
}

public class StoreAssetConditionUpdateHandler : UpdateHandler, IRequestHandler<StoreAssetConditionUpdateCommand, StoreAssetCondition>
{
    public StoreAssetConditionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreAssetCondition> Handle(StoreAssetConditionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssetConditions.SingleOrDefaultAsync(e => e.StoreAssetConditionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetConditionValidator : AbstractValidator<StoreAssetConditionUpdateCommand>
{
    public UpdateStoreAssetConditionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}