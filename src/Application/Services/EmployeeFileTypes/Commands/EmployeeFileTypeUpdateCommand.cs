namespace Engage.Application.Services.EmployeeFileTypes.Commands;

public class EmployeeFileTypeUpdateCommand : IMapTo<EmployeeFileType>, IRequest<EmployeeFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileTypeUpdateCommand, EmployeeFileType>();
    }
}

public class EmployeeFileTypeUpdateHandler : UpdateHandler, IRequestHandler<EmployeeFileTypeUpdateCommand, EmployeeFileType>
{
    public EmployeeFileTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFileType> Handle(EmployeeFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFileTypes.SingleOrDefaultAsync(e => e.EmployeeFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryYearValidator : AbstractValidator<EmployeeFileTypeUpdateCommand>
{
    public UpdateInventoryYearValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}