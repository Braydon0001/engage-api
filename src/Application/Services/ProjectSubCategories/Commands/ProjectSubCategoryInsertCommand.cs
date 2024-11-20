namespace Engage.Application.Services.ProjectSubCategories.Commands;

public class ProjectSubCategoryInsertCommand : IMapTo<ProjectSubCategory>, IRequest<ProjectSubCategory>
{
    public int ProjectCategoryId { get; init; }
    public int? EngageSubGroupId { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubCategoryInsertCommand, ProjectSubCategory>();
    }
}

public record ProjectSubCategoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryInsertCommand, ProjectSubCategory>
{
    public async Task<ProjectSubCategory> Handle(ProjectSubCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectSubCategoryInsertCommand, ProjectSubCategory>(command);

        Context.ProjectSubCategories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectSubCategoryInsertValidator : AbstractValidator<ProjectSubCategoryInsertCommand>
{
    public ProjectSubCategoryInsertValidator()
    {
        RuleFor(e => e.ProjectCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}