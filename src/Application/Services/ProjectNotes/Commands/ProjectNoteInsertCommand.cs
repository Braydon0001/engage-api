namespace Engage.Application.Services.ProjectNotes.Commands;

public class ProjectNoteInsertCommand : IMapTo<ProjectNote>, IRequest<ProjectNote>
{
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectNoteInsertCommand, ProjectNote>();
    }
}

public record ProjectNoteInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectNoteInsertCommand, ProjectNote>
{
    public async Task<ProjectNote> Handle(ProjectNoteInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectNoteInsertCommand, ProjectNote>(command);

        Context.ProjectNotes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectNoteInsertValidator : AbstractValidator<ProjectNoteInsertCommand>
{
    public ProjectNoteInsertValidator()
    {
        RuleFor(e => e.Note).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.ProjectId);
    }
}