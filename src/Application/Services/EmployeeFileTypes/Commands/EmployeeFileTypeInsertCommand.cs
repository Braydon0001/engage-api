namespace Engage.Application.Services.EmployeeFileTypes.Commands;

public class EmployeeFileTypeInsertCommand : IMapTo<EmployeeFileType>, IRequest<EmployeeFileType>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileTypeInsertCommand, EmployeeFileType>();
    }
}

public class EmployeeFileTypeInsertHandler : InsertHandler, IRequestHandler<EmployeeFileTypeInsertCommand, EmployeeFileType>
{
    public EmployeeFileTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFileType> Handle(EmployeeFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeFileTypeInsertCommand, EmployeeFileType>(command);
        _context.EmployeeFileTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryYearInsertValidator : AbstractValidator<EmployeeFileTypeInsertCommand>
{
    public InventoryYearInsertValidator()
    {
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}