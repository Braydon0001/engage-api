namespace Engage.Application.Services.CategorySubGroups.Commands;

public class CategorySubGroupInsertCommand : IMapTo<CategorySubGroup>, IRequest<CategorySubGroup>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategorySubGroupInsertCommand, CategorySubGroup>();
    }
}

public record CategorySubGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategorySubGroupInsertCommand, CategorySubGroup>
{
    public async Task<CategorySubGroup> Handle(CategorySubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategorySubGroupInsertCommand, CategorySubGroup>(command);
        
        Context.CategorySubGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategorySubGroupInsertValidator : AbstractValidator<CategorySubGroupInsertCommand>
{
    public CategorySubGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}