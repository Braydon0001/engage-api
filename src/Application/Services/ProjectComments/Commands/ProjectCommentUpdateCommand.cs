namespace Engage.Application.Services.ProjectComments.Commands;

public class ProjectCommentUpdateCommand : IMapTo<ProjectComment>, IRequest<ProjectComment>
{
    public int Id { get; set; }
    public int ProjectId { get; init; }
    public string Comment { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCommentUpdateCommand, ProjectComment>();
    }
}

public record ProjectCommentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCommentUpdateCommand, ProjectComment>
{
    public async Task<ProjectComment> Handle(ProjectCommentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectComments.SingleOrDefaultAsync(e => e.ProjectCommentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectCommentValidator : AbstractValidator<ProjectCommentUpdateCommand>
{
    public UpdateProjectCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Comment).NotEmpty().MaximumLength(1000);
    }
}