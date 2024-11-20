namespace Engage.Application.Services.CategoryGroups.Commands;

public class CategoryGroupUpdateCommand : IMapTo<CategoryGroup>, IRequest<CategoryGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryGroupUpdateCommand, CategoryGroup>();
    }
}

public record CategoryGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryGroupUpdateCommand, CategoryGroup>
{
    public async Task<CategoryGroup> Handle(CategoryGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryGroups.SingleOrDefaultAsync(e => e.CategoryGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryGroupValidator : AbstractValidator<CategoryGroupUpdateCommand>
{
    public UpdateCategoryGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}