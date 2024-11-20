namespace Engage.Application.Services.CategoryTargetStores.Commands;

public class CategoryTargetStoreInsertCommand : IMapTo<CategoryTargetStore>, IRequest<CategoryTargetStore>
{
    public int CategoryTargetId { get; init; }
    public int StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetStoreInsertCommand, CategoryTargetStore>();
    }
}

public record CategoryTargetStoreInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreInsertCommand, CategoryTargetStore>
{
    public async Task<CategoryTargetStore> Handle(CategoryTargetStoreInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryTargetStoreInsertCommand, CategoryTargetStore>(command);
        
        Context.CategoryTargetStores.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryTargetStoreInsertValidator : AbstractValidator<CategoryTargetStoreInsertCommand>
{
    public CategoryTargetStoreInsertValidator()
    {
        RuleFor(e => e.CategoryTargetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
    }
}