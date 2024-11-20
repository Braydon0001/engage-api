namespace Engage.Application.Services.CategoryTargetTypes.Commands;

public class CategoryTargetTypeUpdateCommand : IMapTo<CategoryTargetType>, IRequest<CategoryTargetType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetTypeUpdateCommand, CategoryTargetType>();
    }
}

public record CategoryTargetTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetTypeUpdateCommand, CategoryTargetType>
{
    public async Task<CategoryTargetType> Handle(CategoryTargetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryTargetTypes.SingleOrDefaultAsync(e => e.CategoryTargetTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryTargetTypeValidator : AbstractValidator<CategoryTargetTypeUpdateCommand>
{
    public UpdateCategoryTargetTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name);
    }
}