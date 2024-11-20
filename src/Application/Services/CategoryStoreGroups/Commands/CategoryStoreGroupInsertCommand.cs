namespace Engage.Application.Services.CategoryStoreGroups.Commands;

public class CategoryStoreGroupInsertCommand : IMapTo<CategoryStoreGroup>, IRequest<List<CategoryStoreGroup>>
{
    public int CategoryGroupId { get; init; }
    public List<int> StoreIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryStoreGroupInsertCommand, CategoryStoreGroup>();
    }
}

public record CategoryStoreGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryStoreGroupInsertCommand, List<CategoryStoreGroup>>
{
    public async Task<List<CategoryStoreGroup>> Handle(CategoryStoreGroupInsertCommand command, CancellationToken cancellationToken)
    {

        List<CategoryStoreGroup> entities = new();
        foreach (var storeId in command.StoreIds)
        {
            entities.Add(new CategoryStoreGroup { StoreId = storeId, CategoryGroupId = command.CategoryGroupId });

        }

        Context.CategoryStoreGroups.AddRange(entities);

        await Context.SaveChangesAsync(cancellationToken);

        return entities;
    }
}

public class CategoryStoreGroupInsertValidator : AbstractValidator<CategoryStoreGroupInsertCommand>
{
    public CategoryStoreGroupInsertValidator()
    {
        RuleFor(e => e.CategoryGroupId).GreaterThan(0);
        RuleFor(e => e.StoreIds).IsNotNull();
    }
}