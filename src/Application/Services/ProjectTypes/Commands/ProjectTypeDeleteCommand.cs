namespace Engage.Application.Services.ProjectTypes.Commands;

public class ProjectTypeDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public record ProjectTypeDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTypeDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTypes.FirstOrDefaultAsync(e => e.ProjectTypeId == command.Id, cancellationToken)
                    ?? throw new Exception("Type not found");

        var subEntities = await Context.ProjectSubTypes.Where(e => e.ProjectTypeId == entity.ProjectTypeId).ToListAsync(cancellationToken);

        var projects = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectTypeId == entity.ProjectTypeId, cancellationToken);

        if (subEntities.Any())
        {
            var subTypeIds = subEntities.Select(e => e.ProjectSubTypeId).ToList();

            var subTypeProjects = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectSubTypeId.HasValue
                                    && subTypeIds.Contains(e.ProjectSubTypeId.Value), cancellationToken);

            if (subTypeProjects != null || projects != null)
            {
                entity.Deleted = true; //if type has subtypes then soft-delete
                foreach (var item in subEntities)
                {
                    item.Deleted = true;
                }
                return await Context.SaveChangesAsync(cancellationToken);
            }
            // None of the subtypes have been used

            Context.ProjectSubTypes.RemoveRange(subEntities);
        }

        if (projects != null)
        {
            entity.Deleted = true; //if type projects attached
            return await Context.SaveChangesAsync(cancellationToken);
        }

        Context.ProjectTypes.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}

public class ProjectTypeDeleteValidator : AbstractValidator<ProjectTypeDeleteCommand>
{
    public ProjectTypeDeleteValidator()
    {
        RuleFor(e => e.Id).NotNull().GreaterThan(0);
    }
}