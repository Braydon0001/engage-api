namespace Engage.Application.Services.ProjectTaskComments.Commands;

public class ProjectTaskCommentInsertCommand : IMapTo<ProjectTaskComment>, IRequest<ProjectTaskComment>
{
    public int ProjectTaskId { get; init; }
    public string Comment { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskCommentInsertCommand, ProjectTaskComment>();
    }
}

public record ProjectTaskCommentInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskCommentInsertCommand, ProjectTaskComment>
{
    public async Task<ProjectTaskComment> Handle(ProjectTaskCommentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskCommentInsertCommand, ProjectTaskComment>(command);
        
        Context.ProjectTaskComments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskCommentInsertValidator : AbstractValidator<ProjectTaskCommentInsertCommand>
{
    public ProjectTaskCommentInsertValidator()
    {
        RuleFor(e => e.ProjectTaskId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Comment).NotEmpty().MaximumLength(1000);
    }
}