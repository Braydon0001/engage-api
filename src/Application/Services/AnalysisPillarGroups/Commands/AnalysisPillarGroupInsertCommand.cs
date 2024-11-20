namespace Engage.Application.Services.AnalysisPillarGroups.Commands;

public class AnalysisPillarGroupInsertCommand : IMapTo<AnalysisPillarGroup>, IRequest<AnalysisPillarGroup>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarGroupInsertCommand, AnalysisPillarGroup>();
    }
}

public record AnalysisPillarGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarGroupInsertCommand, AnalysisPillarGroup>
{
    public async Task<AnalysisPillarGroup> Handle(AnalysisPillarGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<AnalysisPillarGroupInsertCommand, AnalysisPillarGroup>(command);
        
        Context.AnalysisPillarGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class AnalysisPillarGroupInsertValidator : AbstractValidator<AnalysisPillarGroupInsertCommand>
{
    public AnalysisPillarGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}