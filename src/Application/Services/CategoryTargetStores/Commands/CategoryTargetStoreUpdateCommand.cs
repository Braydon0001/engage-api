namespace Engage.Application.Services.CategoryTargetStores.Commands;

public class CategoryTargetStoreUpdateCommand : IMapTo<CategoryTargetStore>, IRequest<CategoryTargetStore>
{
    public int Id { get; set; }
    public int CategoryTargetId { get; init; }
    public int StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetStoreUpdateCommand, CategoryTargetStore>();
    }
}

public record CategoryTargetStoreUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreUpdateCommand, CategoryTargetStore>
{
    public async Task<CategoryTargetStore> Handle(CategoryTargetStoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryTargetStores.SingleOrDefaultAsync(e => e.CategoryTargetStoreId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryTargetStoreValidator : AbstractValidator<CategoryTargetStoreUpdateCommand>
{
    public UpdateCategoryTargetStoreValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CategoryTargetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
    }
}