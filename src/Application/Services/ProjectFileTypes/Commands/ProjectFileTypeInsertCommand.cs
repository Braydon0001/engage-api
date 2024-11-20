namespace Engage.Application.Services.ProjectFileTypes.Commands;

public class ProjectFileTypeInsertCommand : IMapTo<ProjectFileType>, IRequest<ProjectFileType>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileTypeInsertCommand, ProjectFileType>();
    }
}

public class ProjectFileTypeInsertHandler : InsertHandler, IRequestHandler<ProjectFileTypeInsertCommand, ProjectFileType>
{
    public ProjectFileTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFileType> Handle(ProjectFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProjectFileTypeInsertCommand, ProjectFileType>(command);
        _context.ProjectFileTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryYearInsertValidator : AbstractValidator<ProjectFileTypeInsertCommand>
{
    public InventoryYearInsertValidator()
    {
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}