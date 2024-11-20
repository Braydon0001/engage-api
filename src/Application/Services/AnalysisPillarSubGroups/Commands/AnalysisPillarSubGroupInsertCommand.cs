namespace Engage.Application.Services.AnalysisPillarSubGroups.Commands;

public class AnalysisPillarSubGroupInsertCommand : IMapTo<AnalysisPillarSubGroup>, IRequest<AnalysisPillarSubGroup>
{
    public int AnalysisPillarGroupId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarSubGroupInsertCommand, AnalysisPillarSubGroup>();
    }
}

public record AnalysisPillarSubGroupInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarSubGroupInsertCommand, AnalysisPillarSubGroup>
{
    public async Task<AnalysisPillarSubGroup> Handle(AnalysisPillarSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<AnalysisPillarSubGroupInsertCommand, AnalysisPillarSubGroup>(command);
        
        Context.AnalysisPillarSubGroups.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class AnalysisPillarSubGroupInsertValidator : AbstractValidator<AnalysisPillarSubGroupInsertCommand>
{
    public AnalysisPillarSubGroupInsertValidator()
    {
        RuleFor(e => e.AnalysisPillarGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}