namespace Engage.Application.Services.ProjectCampaigns.Commands;

public class ProjectCampaignUpdateCommand : IMapTo<ProjectCampaign>, IRequest<ProjectCampaign>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public List<JsonText> Note { get; init; }
    public int? EngageRegionId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCampaignUpdateCommand, ProjectCampaign>();
    }
}

public record ProjectCampaignUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCampaignUpdateCommand, ProjectCampaign>
{
    public async Task<ProjectCampaign> Handle(ProjectCampaignUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectCampaigns.SingleOrDefaultAsync(e => e.ProjectCampaignId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }
        entity.Note = null;
        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectCampaignValidator : AbstractValidator<ProjectCampaignUpdateCommand>
{
    public UpdateProjectCampaignValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}