namespace Engage.Application.Services.SparAnalysisGroups.Commands;

public class SparAnalysisGroupInsertCommand : IMapTo<SparAnalysisGroup>, IRequest<SparAnalysisGroup>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparAnalysisGroupInsertCommand, SparAnalysisGroup>();
    }
}

public record SparAnalysisGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparAnalysisGroupInsertCommand, SparAnalysisGroup>
{
    public async Task<SparAnalysisGroup> Handle(SparAnalysisGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparAnalysisGroupInsertCommand, SparAnalysisGroup>(command);
        
        Context.SparAnalysisGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparAnalysisGroupInsertValidator : AbstractValidator<SparAnalysisGroupInsertCommand>
{
    public SparAnalysisGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}