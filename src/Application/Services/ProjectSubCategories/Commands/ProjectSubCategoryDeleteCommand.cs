namespace Engage.Application.Services.ProjectSubCategories.Commands;

public class ProjectSubCategoryDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public record ProjectSubCategoryDeleteHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryDeleteCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectSubCategoryDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectSubCategories.SingleOrDefaultAsync(e => e.ProjectSubCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var project = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectSubCategoryId == command.Id, cancellationToken);

        if (project != null)
        {
            entity.Deleted = true;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        Context.ProjectSubCategories.Remove(entity);

        return await Context.SaveChangesAsync(cancellationToken);
    }
}

public class ProjectSubCategoryDeleteValidator : AbstractValidator<ProjectSubCategoryDeleteCommand>
{
    public ProjectSubCategoryDeleteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}