namespace Engage.Application.Services.Projects.Commands;

public class ProjectUpdateCommand : IMapTo<Project>, IRequest<Project>
{
    public int Id { get; set; }
    public string Name { get; init; }
    //public List<JsonText> Note { get; init; }
    public int ProjectTypeId { get; init; }
    public int ProjectPriorityId { get; set; }
    public int? EngageRegionId { get; init; }
    public int? ProjectCampaignId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectUpdateCommand, Project>();
    }
}

public record ProjectUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectUpdateCommand, Project>
{
    public async Task<Project> Handle(ProjectUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
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

public class UpdateProjectValidator : AbstractValidator<ProjectUpdateCommand>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        //RuleFor(e => e.Note);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.ProjectCampaignId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}