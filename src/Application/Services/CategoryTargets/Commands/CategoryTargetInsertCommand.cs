namespace Engage.Application.Services.CategoryTargets.Commands;

public class CategoryTargetInsertCommand : IMapTo<CategoryTarget>, IRequest<CategoryTarget>
{
    public int SupplierId { get; init; }
    public float Target { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public string TextQuestion { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetInsertCommand, CategoryTarget>();
    }
}

public record CategoryTargetInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetInsertCommand, CategoryTarget>
{
    public async Task<CategoryTarget> Handle(CategoryTargetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CategoryTargetInsertCommand, CategoryTarget>(command);

        Context.CategoryTargets.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CategoryTargetInsertValidator : AbstractValidator<CategoryTargetInsertCommand>
{
    public CategoryTargetInsertValidator()
    {

        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Target).NotEmpty();
        RuleFor(e => e.AvailableLabel).NotEmpty().MaximumLength(100);
        RuleFor(e => e.OccupiedLabel).NotEmpty().MaximumLength(100);
    }
}