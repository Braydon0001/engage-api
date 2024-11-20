namespace Engage.Application.Services.ProjectUsers.Commands;

public class ProjectUserInsertCommand : IMapTo<ProjectUser>, IRequest<ProjectUser>
{
    public int ProjectId { get; init; }
    public int UserId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectUserInsertCommand, ProjectUser>();
    }
}

public record ProjectUserInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectUserInsertCommand, ProjectUser>
{
    public async Task<ProjectUser> Handle(ProjectUserInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectUserInsertCommand, ProjectUser>(command);

        Context.ProjectUsers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectUserInsertValidator : AbstractValidator<ProjectUserInsertCommand>
{
    public ProjectUserInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserId).NotEmpty().GreaterThan(0);
    }
}