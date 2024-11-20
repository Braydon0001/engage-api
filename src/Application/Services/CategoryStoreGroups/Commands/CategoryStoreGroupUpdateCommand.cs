namespace Engage.Application.Services.CategoryStoreGroups.Commands;

public class CategoryStoreGroupUpdateCommand : IMapTo<CategoryStoreGroup>, IRequest<CategoryStoreGroup>
{
    public int Id { get; set; }
    public int CategoryGroupId { get; init; }
    public int StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryStoreGroupUpdateCommand, CategoryStoreGroup>();
    }
}

public record CategoryStoreGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryStoreGroupUpdateCommand, CategoryStoreGroup>
{
    public async Task<CategoryStoreGroup> Handle(CategoryStoreGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryStoreGroups.SingleOrDefaultAsync(e => e.CategoryStoreGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryStoreGroupValidator : AbstractValidator<CategoryStoreGroupUpdateCommand>
{
    public UpdateCategoryStoreGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CategoryGroupId).GreaterThan(0);
        RuleFor(e => e.StoreId).GreaterThan(0);
    }
}