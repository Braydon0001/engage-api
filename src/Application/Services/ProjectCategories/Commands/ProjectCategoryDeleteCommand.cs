namespace Engage.Application.Services.ProjectCategories.Commands;

public class ProjectCategoryDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public record ProjectCategoryDeleteHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCategoryDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectCategoryDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectCategories.SingleOrDefaultAsync(e => e.ProjectCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var subCategories = await Context.ProjectSubCategories.Where(e => e.ProjectCategoryId == command.Id).ToListAsync(cancellationToken);

        var project = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectCategoryId == command.Id, cancellationToken);

        if (subCategories.Any())
        {
            var subCategoryIds = subCategories.Select(e => e.ProjectSubCategoryId).ToList();

            var subTypeProjects = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectSubCategoryId.HasValue
                        && subCategoryIds.Contains(e.ProjectSubCategoryId.Value), cancellationToken);

            if (subTypeProjects != null || project != null)
            {
                entity.Deleted = true;
                foreach (var item in subCategories)
                {
                    item.Deleted = true;
                }
                return await Context.SaveChangesAsync(cancellationToken);
            }

            Context.ProjectSubCategories.RemoveRange(subCategories);
        }

        if (project != null)
        {
            entity.Deleted = true;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        Context.ProjectCategories.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}

public class ProjectCategoryDeleteValidator : AbstractValidator<ProjectCategoryDeleteCommand>
{
    public ProjectCategoryDeleteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}