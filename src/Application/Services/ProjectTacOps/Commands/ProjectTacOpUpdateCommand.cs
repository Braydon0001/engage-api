namespace Engage.Application.Services.ProjectTacOps.Commands;

public class ProjectTacOpUpdateCommand : IMapTo<ProjectTacOp>, IRequest<ProjectTacOp>
{
    public int Id { get; set; }
    public int UserId { get; init; }
    public string MobilePhone { get; init; }
    public List<int> EngageRegionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTacOpUpdateCommand, ProjectTacOp>();
    }
}

public record ProjectTacOpUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectTacOpUpdateCommand, ProjectTacOp>
{
    public async Task<ProjectTacOp> Handle(ProjectTacOpUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTacOps.SingleOrDefaultAsync(e => e.ProjectTacOpId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        var existingRegions = await Context.ProjectTacOpRegions.Where(e => e.ProjectTacOpId == entity.ProjectTacOpId)
                                                               .ToListAsync(cancellationToken);

        var regionsToRemove = existingRegions.Where(e => !command.EngageRegionIds.Contains(e.EngageRegionId))
                                             .ToList();

        var regionsToAdd = command.EngageRegionIds.Where(e => !existingRegions.Select(x => x.EngageRegionId).Contains(e))
                                                  .ToList();


        if (regionsToAdd != null && regionsToAdd.Count > 0)
        {
            foreach (var regionId in regionsToAdd)
            {
                var region = new ProjectTacOpRegion
                {
                    ProjectTacOp = entity,
                    EngageRegionId = regionId
                };

                entity.ProjectTacOpRegions.Add(region);
            }
        }

        if (regionsToRemove != null && regionsToRemove.Count > 0)
        {
            foreach (var region in regionsToRemove)
            {
                Context.ProjectTacOpRegions.Remove(region);
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTacOpValidator : AbstractValidator<ProjectTacOpUpdateCommand>
{
    public UpdateProjectTacOpValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.MobilePhone).MaximumLength(20);
        RuleFor(e => e.EngageRegionIds);
    }
}