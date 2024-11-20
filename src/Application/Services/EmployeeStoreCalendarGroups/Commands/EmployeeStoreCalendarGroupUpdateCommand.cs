// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarGroups.Commands;

public class EmployeeStoreCalendarGroupUpdateCommand : IMapTo<EmployeeStoreCalendarGroup>, IRequest<EmployeeStoreCalendarGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarGroupUpdateCommand, EmployeeStoreCalendarGroup>();
    }
}

public class EmployeeStoreCalendarGroupUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarGroupUpdateCommand, EmployeeStoreCalendarGroup>
{
    public EmployeeStoreCalendarGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarGroup> Handle(EmployeeStoreCalendarGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarGroups.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarGroupValidator : AbstractValidator<EmployeeStoreCalendarGroupUpdateCommand>
{
    public UpdateEmployeeStoreCalendarGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
    }
}