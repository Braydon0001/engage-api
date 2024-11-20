namespace Engage.Application.Services.ProjectFileTypes.Commands;

public class ProjectFileTypeUpdateCommand : IMapTo<ProjectFileType>, IRequest<ProjectFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileTypeUpdateCommand, ProjectFileType>();
    }
}

public class ProjectFileTypeUpdateHandler : UpdateHandler, IRequestHandler<ProjectFileTypeUpdateCommand, ProjectFileType>
{
    public ProjectFileTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFileType> Handle(ProjectFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectFileTypes.SingleOrDefaultAsync(e => e.ProjectFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryYearValidator : AbstractValidator<ProjectFileTypeUpdateCommand>
{
    public UpdateInventoryYearValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}