namespace Engage.Application.Services.CategoryFileTypes.Commands;

public class CategoryFileTypeInsertCommand : IMapTo<CategoryFileType>, IRequest<CategoryFileType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFileTypeInsertCommand, CategoryFileType>();
    }
}

public record CategoryFileTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTypeInsertCommand, CategoryFileType>
{
    public async Task<CategoryFileType> Handle(CategoryFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryFileTypeInsertCommand, CategoryFileType>(command);
        
        Context.CategoryFileTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryFileTypeInsertValidator : AbstractValidator<CategoryFileTypeInsertCommand>
{
    public CategoryFileTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}