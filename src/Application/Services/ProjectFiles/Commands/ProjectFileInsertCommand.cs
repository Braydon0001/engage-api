namespace Engage.Application.Services.ProjectFiles.Commands;

public class ProjectFileInsertCommand : IMapTo<ProjectFile>, IRequest<ProjectFile>
{
    public int ProjectId { get; set; }
    public int ProjectFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileInsertCommand, ProjectFile>();
    }
}

public class ProjectFileInsertHandler : InsertHandler, IRequestHandler<ProjectFileInsertCommand, ProjectFile>
{
    public ProjectFileInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFile> Handle(ProjectFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProjectFileInsertCommand, ProjectFile>(command);

        _context.ProjectFiles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectFileInsertValidator : AbstractValidator<ProjectFileInsertCommand>
{
    public ProjectFileInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectFileTypeId).NotEmpty().GreaterThan(0);
    }
}