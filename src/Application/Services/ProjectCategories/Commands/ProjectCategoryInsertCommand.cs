using Engage.Application.Services.ProjectCategorySuppliers.Commands;

namespace Engage.Application.Services.ProjectCategories.Commands;

public class ProjectCategoryInsertCommand : IMapTo<ProjectCategory>, IRequest<ProjectCategory>
{
    public string Name { get; init; }
    public List<int> SupplierIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategoryInsertCommand, ProjectCategory>();
    }
}

public record ProjectCategoryInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCategoryInsertCommand, ProjectCategory>
{
    public async Task<ProjectCategory> Handle(ProjectCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectCategoryInsertCommand, ProjectCategory>(command);

        Context.ProjectCategories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        if (command.SupplierIds.IsNotNullOrEmpty())
        {
            await Mediator.Send(new ProjectCategorySupplierUpdateCommand
            {
                ProjectCategoryId = entity.ProjectCategoryId,
                SupplierIds = command.SupplierIds
            }, cancellationToken);
        }

        return entity;
    }
}

public class ProjectCategoryInsertValidator : AbstractValidator<ProjectCategoryInsertCommand>
{
    public ProjectCategoryInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}