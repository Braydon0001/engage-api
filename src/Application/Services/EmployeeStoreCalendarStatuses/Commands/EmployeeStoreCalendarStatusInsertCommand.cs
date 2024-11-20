// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Commands;

public class EmployeeStoreCalendarStatusInsertCommand : IMapTo<EmployeeStoreCalendarStatus>, IRequest<EmployeeStoreCalendarStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarStatusInsertCommand, EmployeeStoreCalendarStatus>();
    }
}

public class EmployeeStoreCalendarStatusInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarStatusInsertCommand, EmployeeStoreCalendarStatus>
{
    public EmployeeStoreCalendarStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarStatus> Handle(EmployeeStoreCalendarStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeStoreCalendarStatusInsertCommand, EmployeeStoreCalendarStatus>(command);
        
        _context.EmployeeStoreCalendarStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarStatusInsertValidator : AbstractValidator<EmployeeStoreCalendarStatusInsertCommand>
{
    public EmployeeStoreCalendarStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}