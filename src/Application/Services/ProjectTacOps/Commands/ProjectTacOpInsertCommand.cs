namespace Engage.Application.Services.ProjectTacOps.Commands;

public class ProjectTacOpInsertCommand : IMapTo<ProjectTacOp>, IRequest<ProjectTacOp>
{
    public int UserId { get; init; }
    public string MobilePhone { get; init; }
    public List<int> EngageRegionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTacOpInsertCommand, ProjectTacOp>();
    }
}

public record ProjectTacOpInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectTacOpInsertCommand, ProjectTacOp>
{
    public async Task<ProjectTacOp> Handle(ProjectTacOpInsertCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await Context.ProjectTacOps.Where(e => e.UserId == command.UserId)
                                                      .FirstOrDefaultAsync(cancellationToken);

        if (existingUser != null)
        {
            throw new Exception("User is already Tac-Ops");
        }

        var entity = Mapper.Map<ProjectTacOpInsertCommand, ProjectTacOp>(command);

        Context.ProjectTacOps.Add(entity);

        if (command.EngageRegionIds != null && command.EngageRegionIds.Count > 0)
        {
            foreach (var regionId in command.EngageRegionIds)
            {
                var region = new ProjectTacOpRegion
                {
                    ProjectTacOp = entity,
                    EngageRegionId = regionId
                };

                entity.ProjectTacOpRegions.Add(region);
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTacOpInsertValidator : AbstractValidator<ProjectTacOpInsertCommand>
{
    public ProjectTacOpInsertValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.MobilePhone).MaximumLength(20);
        RuleFor(e => e.EngageRegionIds);
    }
}