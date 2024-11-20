// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Commands;

public class EmployeeStoreCalendarGroupInsertCommand : IMapTo<EmployeeStoreCalendarGroup>, IRequest<EmployeeStoreCalendarGroup>
{
    public string Name { get; set; }
    public int Number { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarGroupInsertCommand, EmployeeStoreCalendarGroup>();
    }
}

public class EmployeeStoreCalendarGroupInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarGroupInsertCommand, EmployeeStoreCalendarGroup>
{
    public EmployeeStoreCalendarGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarGroup> Handle(EmployeeStoreCalendarGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeStoreCalendarGroupInsertCommand, EmployeeStoreCalendarGroup>(command);
        
        _context.EmployeeStoreCalendarGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarGroupInsertValidator : AbstractValidator<EmployeeStoreCalendarGroupInsertCommand>
{
    public EmployeeStoreCalendarGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
    }
}