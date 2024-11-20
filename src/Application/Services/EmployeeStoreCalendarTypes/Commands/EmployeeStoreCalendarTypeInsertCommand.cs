// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Commands;

public class EmployeeStoreCalendarTypeInsertCommand : IMapTo<EmployeeStoreCalendarType>, IRequest<EmployeeStoreCalendarType>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarTypeInsertCommand, EmployeeStoreCalendarType>();
    }
}

public class EmployeeStoreCalendarTypeInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarTypeInsertCommand, EmployeeStoreCalendarType>
{
    public EmployeeStoreCalendarTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarType> Handle(EmployeeStoreCalendarTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeStoreCalendarTypeInsertCommand, EmployeeStoreCalendarType>(command);
        
        _context.EmployeeStoreCalendarTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarTypeInsertValidator : AbstractValidator<EmployeeStoreCalendarTypeInsertCommand>
{
    public EmployeeStoreCalendarTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}