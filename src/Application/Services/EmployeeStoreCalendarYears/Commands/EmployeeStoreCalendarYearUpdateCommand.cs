// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Commands;

public class EmployeeStoreCalendarYearUpdateCommand : IMapTo<EmployeeStoreCalendarYear>, IRequest<EmployeeStoreCalendarYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarYearUpdateCommand, EmployeeStoreCalendarYear>();
    }
}

public class EmployeeStoreCalendarYearUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarYearUpdateCommand, EmployeeStoreCalendarYear>
{
    public EmployeeStoreCalendarYearUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarYear> Handle(EmployeeStoreCalendarYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarYears.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarYearValidator : AbstractValidator<EmployeeStoreCalendarYearUpdateCommand>
{
    public UpdateEmployeeStoreCalendarYearValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}