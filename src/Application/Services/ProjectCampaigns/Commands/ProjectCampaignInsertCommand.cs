namespace Engage.Application.Services.ProjectCampaigns.Commands;

public class ProjectCampaignInsertCommand : IMapTo<ProjectCampaign>, IRequest<ProjectCampaign>
{
    public string Name { get; init; }
    public List<JsonText> Note { get; init; }
    public int? EngageRegionId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCampaignInsertCommand, ProjectCampaign>();
    }
}

public record ProjectCampaignInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCampaignInsertCommand, ProjectCampaign>
{
    public async Task<ProjectCampaign> Handle(ProjectCampaignInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectCampaignInsertCommand, ProjectCampaign>(command);

        Context.ProjectCampaigns.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectCampaignInsertValidator : AbstractValidator<ProjectCampaignInsertCommand>
{
    public ProjectCampaignInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}