namespace Engage.Application.Services.ProjectNotes.Commands;

public class ProjectNoteUpdateCommand : IMapTo<ProjectNote>, IRequest<ProjectNote>
{
    public int Id { get; set; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectNoteUpdateCommand, ProjectNote>();
    }
}

public record ProjectNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectNoteUpdateCommand, ProjectNote>
{
    public async Task<ProjectNote> Handle(ProjectNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectNotes.SingleOrDefaultAsync(e => e.ProjectNoteId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectNoteValidator : AbstractValidator<ProjectNoteUpdateCommand>
{
    public UpdateProjectNoteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
    }
}