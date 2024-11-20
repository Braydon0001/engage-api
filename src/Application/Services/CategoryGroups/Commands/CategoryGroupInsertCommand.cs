namespace Engage.Application.Services.CategoryGroups.Commands;

public class CategoryGroupInsertCommand : IMapTo<CategoryGroup>, IRequest<CategoryGroup>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryGroupInsertCommand, CategoryGroup>();
    }
}

public record CategoryGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryGroupInsertCommand, CategoryGroup>
{
    public async Task<CategoryGroup> Handle(CategoryGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryGroupInsertCommand, CategoryGroup>(command);
        
        Context.CategoryGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryGroupInsertValidator : AbstractValidator<CategoryGroupInsertCommand>
{
    public CategoryGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}