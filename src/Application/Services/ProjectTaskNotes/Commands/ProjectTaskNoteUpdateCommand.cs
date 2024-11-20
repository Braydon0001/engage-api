namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public class ProjectTaskNoteUpdateCommand : IMapTo<ProjectTaskNote>, IRequest<ProjectTaskNote>
{
    public int Id { get; set; }
    public string Note { get; init; }
    public int ProjectTaskId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskNoteUpdateCommand, ProjectTaskNote>();
    }
}

public record ProjectTaskNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNoteUpdateCommand, ProjectTaskNote>
{
    public async Task<ProjectTaskNote> Handle(ProjectTaskNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskNotes.SingleOrDefaultAsync(e => e.ProjectTaskNoteId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskNoteValidator : AbstractValidator<ProjectTaskNoteUpdateCommand>
{
    public UpdateProjectTaskNoteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.ProjectTaskId).NotEmpty().GreaterThan(0);
    }
}