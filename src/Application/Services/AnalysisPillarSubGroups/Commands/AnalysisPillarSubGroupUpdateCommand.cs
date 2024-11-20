namespace Engage.Application.Services.AnalysisPillarSubGroups.Commands;

public class AnalysisPillarSubGroupUpdateCommand : IMapTo<AnalysisPillarSubGroup>, IRequest<AnalysisPillarSubGroup>
{
    public int Id { get; set; }
    public int AnalysisPillarGroupId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnalysisPillarSubGroupUpdateCommand, AnalysisPillarSubGroup>();
    }
}

public record AnalysisPillarSubGroupUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarSubGroupUpdateCommand, AnalysisPillarSubGroup>
{
    public async Task<AnalysisPillarSubGroup> Handle(AnalysisPillarSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.AnalysisPillarSubGroups.SingleOrDefaultAsync(e => e.AnalysisPillarSubGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateAnalysisPillarSubGroupValidator : AbstractValidator<AnalysisPillarSubGroupUpdateCommand>
{
    public UpdateAnalysisPillarSubGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnalysisPillarGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}