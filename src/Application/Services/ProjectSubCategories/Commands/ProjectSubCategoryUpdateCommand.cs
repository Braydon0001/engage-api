namespace Engage.Application.Services.ProjectSubCategories.Commands;

public class ProjectSubCategoryUpdateCommand : IMapTo<ProjectSubCategory>, IRequest<ProjectSubCategory>
{
    public int Id { get; set; }
    public int ProjectCategoryId { get; init; }
    public int? EngageSubGroupId { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubCategoryUpdateCommand, ProjectSubCategory>();
    }
}

public record ProjectSubCategoryUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryUpdateCommand, ProjectSubCategory>
{
    public async Task<ProjectSubCategory> Handle(ProjectSubCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectSubCategories.SingleOrDefaultAsync(e => e.ProjectSubCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectSubCategoryValidator : AbstractValidator<ProjectSubCategoryUpdateCommand>
{
    public UpdateProjectSubCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}