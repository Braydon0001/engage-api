namespace Engage.Application.Services.EngageBrands.Commands;

public class EngageBrandInsertCommand : IMapTo<EngageBrand>, IRequest<EngageBrand>
{
    public string Name { get; init; }
    public bool IsSparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageBrandInsertCommand, EngageBrand>();
    }
}

public record EngageBrandInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageBrandInsertCommand, EngageBrand>
{
    public async Task<EngageBrand> Handle(EngageBrandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EngageBrandInsertCommand, EngageBrand>(command);

        Context.EngageBrands.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EngageBrandInsertValidator : AbstractValidator<EngageBrandInsertCommand>
{
    public EngageBrandInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}