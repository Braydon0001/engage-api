namespace Engage.Application.Services.EngageBrands.Commands;

public class EngageBrandUpdateCommand : IMapTo<EngageBrand>, IRequest<EngageBrand>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public bool IsSparBrand { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageBrandUpdateCommand, EngageBrand>();
    }
}

public record EngageBrandUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageBrandUpdateCommand, EngageBrand>
{
    public async Task<EngageBrand> Handle(EngageBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EngageBrands.SingleOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEngageBrandValidator : AbstractValidator<EngageBrandUpdateCommand>
{
    public UpdateEngageBrandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}