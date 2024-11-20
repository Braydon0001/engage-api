namespace Engage.Application.Services.CategorySubGroups.Commands;

public class CategorySubGroupUpdateCommand : IMapTo<CategorySubGroup>, IRequest<CategorySubGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategorySubGroupUpdateCommand, CategorySubGroup>();
    }
}

public record CategorySubGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategorySubGroupUpdateCommand, CategorySubGroup>
{
    public async Task<CategorySubGroup> Handle(CategorySubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategorySubGroups.SingleOrDefaultAsync(e => e.CategorySubGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategorySubGroupValidator : AbstractValidator<CategorySubGroupUpdateCommand>
{
    public UpdateCategorySubGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}