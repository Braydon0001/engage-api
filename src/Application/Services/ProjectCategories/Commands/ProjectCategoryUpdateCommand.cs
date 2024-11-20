using Engage.Application.Services.ProjectCategorySuppliers.Commands;

namespace Engage.Application.Services.ProjectCategories.Commands;

public class ProjectCategoryUpdateCommand : IMapTo<ProjectCategory>, IRequest<ProjectCategory>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public List<int> SupplierIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategoryUpdateCommand, ProjectCategory>();
    }
}

public record ProjectCategoryUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCategoryUpdateCommand, ProjectCategory>
{
    public async Task<ProjectCategory> Handle(ProjectCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectCategories.SingleOrDefaultAsync(e => e.ProjectCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        if (command.SupplierIds.IsNotNullOrEmpty())
        {
            await Mediator.Send(new ProjectCategorySupplierUpdateCommand
            {
                ProjectCategoryId = entity.ProjectCategoryId,
                SupplierIds = command.SupplierIds
            }, cancellationToken);
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectCategoryValidator : AbstractValidator<ProjectCategoryUpdateCommand>
{
    public UpdateProjectCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}