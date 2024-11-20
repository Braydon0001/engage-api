namespace Engage.Application.Services.CategoryFileTypes.Commands;

public class CategoryFileTypeUpdateCommand : IMapTo<CategoryFileType>, IRequest<CategoryFileType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFileTypeUpdateCommand, CategoryFileType>();
    }
}

public record CategoryFileTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTypeUpdateCommand, CategoryFileType>
{
    public async Task<CategoryFileType> Handle(CategoryFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryFileTypes.SingleOrDefaultAsync(e => e.CategoryFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryFileTypeValidator : AbstractValidator<CategoryFileTypeUpdateCommand>
{
    public UpdateCategoryFileTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}