namespace Engage.Application.Services.ProjectFiles.Commands;

public class ProjectFileUpdateCommand : IMapTo<ProjectFile>, IRequest<ProjectFile>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int ProjectFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileUpdateCommand, ProjectFile>();
    }
}

public class ProjectFileUpdateHandler : UpdateHandler, IRequestHandler<ProjectFileUpdateCommand, ProjectFile>
{
    public ProjectFileUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFile> Handle(ProjectFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectFiles.SingleOrDefaultAsync(e => e.ProjectFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectFileValidator : AbstractValidator<ProjectFileUpdateCommand>
{
    public UpdateProjectFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectFileTypeId).NotEmpty().GreaterThan(0);
    }
}