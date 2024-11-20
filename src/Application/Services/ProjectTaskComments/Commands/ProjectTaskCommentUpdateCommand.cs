namespace Engage.Application.Services.ProjectTaskComments.Commands;

public class ProjectTaskCommentUpdateCommand : IMapTo<ProjectTaskComment>, IRequest<ProjectTaskComment>
{
    public int Id { get; set; }
    public int ProjectTaskId { get; init; }
    public string Comment { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskCommentUpdateCommand, ProjectTaskComment>();
    }
}

public record ProjectTaskCommentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskCommentUpdateCommand, ProjectTaskComment>
{
    public async Task<ProjectTaskComment> Handle(ProjectTaskCommentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskComments.SingleOrDefaultAsync(e => e.ProjectTaskCommentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskCommentValidator : AbstractValidator<ProjectTaskCommentUpdateCommand>
{
    public UpdateProjectTaskCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Comment).NotEmpty().MaximumLength(1000);
    }
}