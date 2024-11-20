namespace Engage.Application.Services.ProjectSubTypes.Commands;

public class ProjectSubTypeDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public record ProjectSubTypeDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectSubTypeDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectSubTypes.SingleOrDefaultAsync(e => e.ProjectSubTypeId == command.Id, cancellationToken)
            ?? throw new Exception("Sub-Type not found");

        var projects = await Context.Projects.Where(e => e.ProjectSubTypeId == entity.ProjectSubTypeId).ToListAsync(cancellationToken);

        if (projects.Any())
        {
            entity.Deleted = true;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        Context.ProjectSubTypes.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}

public class ProjectSubTypeDeleteValidator : AbstractValidator<ProjectSubTypeDeleteCommand>
{
    public ProjectSubTypeDeleteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}