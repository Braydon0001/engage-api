namespace Engage.Application.Services.CategoryTargets.Commands;

public class CategoryTargetUpdateCommand : IMapTo<CategoryTarget>, IRequest<CategoryTarget>
{
    public int Id { get; set; }
    public int SupplierId { get; init; }
    public float Target { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public string TextQuestion { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetUpdateCommand, CategoryTarget>();
    }
}

public record CategoryTargetUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetUpdateCommand, CategoryTarget>
{
    public async Task<CategoryTarget> Handle(CategoryTargetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryTargets.SingleOrDefaultAsync(e => e.CategoryTargetId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCategoryTargetValidator : AbstractValidator<CategoryTargetUpdateCommand>
{
    public UpdateCategoryTargetValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Target).NotEmpty();
        RuleFor(e => e.AvailableLabel).NotEmpty().MaximumLength(100);
        RuleFor(e => e.OccupiedLabel).NotEmpty().MaximumLength(100);
    }
}