namespace Engage.Application.Services.AnalysisPillarGroups.Commands;

public class AnalysisPillarGroupUpdateCommand : IMapTo<AnalysisPillarGroup>, IRequest<AnalysisPillarGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarGroupUpdateCommand, AnalysisPillarGroup>();
    }
}

public record AnalysisPillarGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarGroupUpdateCommand, AnalysisPillarGroup>
{
    public async Task<AnalysisPillarGroup> Handle(AnalysisPillarGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.AnalysisPillarGroups.SingleOrDefaultAsync(e => e.AnalysisPillarGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateAnalysisPillarGroupValidator : AbstractValidator<AnalysisPillarGroupUpdateCommand>
{
    public UpdateAnalysisPillarGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}