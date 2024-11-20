namespace Engage.Application.Services.SparAnalysisGroups.Commands;

public class SparAnalysisGroupUpdateCommand : IMapTo<SparAnalysisGroup>, IRequest<SparAnalysisGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparAnalysisGroupUpdateCommand, SparAnalysisGroup>();
    }
}

public record SparAnalysisGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparAnalysisGroupUpdateCommand, SparAnalysisGroup>
{
    public async Task<SparAnalysisGroup> Handle(SparAnalysisGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparAnalysisGroups.SingleOrDefaultAsync(e => e.SparAnalysisGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparAnalysisGroupValidator : AbstractValidator<SparAnalysisGroupUpdateCommand>
{
    public UpdateSparAnalysisGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}