// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarStatuses.Commands;

public class EmployeeStoreCalendarStatusUpdateCommand : IMapTo<EmployeeStoreCalendarStatus>, IRequest<EmployeeStoreCalendarStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarStatusUpdateCommand, EmployeeStoreCalendarStatus>();
    }
}

public class EmployeeStoreCalendarStatusUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarStatusUpdateCommand, EmployeeStoreCalendarStatus>
{
    public EmployeeStoreCalendarStatusUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarStatus> Handle(EmployeeStoreCalendarStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarStatuses.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarStatusValidator : AbstractValidator<EmployeeStoreCalendarStatusUpdateCommand>
{
    public UpdateEmployeeStoreCalendarStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}