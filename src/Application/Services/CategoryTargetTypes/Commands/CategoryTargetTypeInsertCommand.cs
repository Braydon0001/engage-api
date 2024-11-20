namespace Engage.Application.Services.CategoryTargetTypes.Commands;

public class CategoryTargetTypeInsertCommand : IMapTo<CategoryTargetType>, IRequest<CategoryTargetType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetTypeInsertCommand, CategoryTargetType>();
    }
}

public record CategoryTargetTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetTypeInsertCommand, CategoryTargetType>
{
    public async Task<CategoryTargetType> Handle(CategoryTargetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryTargetTypeInsertCommand, CategoryTargetType>(command);
        
        Context.CategoryTargetTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryTargetTypeInsertValidator : AbstractValidator<CategoryTargetTypeInsertCommand>
{
    public CategoryTargetTypeInsertValidator()
    {
        RuleFor(e => e.Name);
    }
}