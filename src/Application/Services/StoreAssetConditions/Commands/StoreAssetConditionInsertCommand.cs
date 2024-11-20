// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Commands;

public class StoreAssetConditionInsertCommand : IMapTo<StoreAssetCondition>, IRequest<StoreAssetCondition>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetConditionInsertCommand, StoreAssetCondition>();
    }
}

public class StoreAssetConditionInsertHandler : InsertHandler, IRequestHandler<StoreAssetConditionInsertCommand, StoreAssetCondition>
{
    public StoreAssetConditionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreAssetCondition> Handle(StoreAssetConditionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreAssetConditionInsertCommand, StoreAssetCondition>(command);
        
        _context.StoreAssetConditions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreAssetConditionInsertValidator : AbstractValidator<StoreAssetConditionInsertCommand>
{
    public StoreAssetConditionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}