// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarTypes.Commands;

public class EmployeeStoreCalendarTypeUpdateCommand : IMapTo<EmployeeStoreCalendarType>, IRequest<EmployeeStoreCalendarType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarTypeUpdateCommand, EmployeeStoreCalendarType>();
    }
}

public class EmployeeStoreCalendarTypeUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarTypeUpdateCommand, EmployeeStoreCalendarType>
{
    public EmployeeStoreCalendarTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarType> Handle(EmployeeStoreCalendarTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarTypes.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarTypeValidator : AbstractValidator<EmployeeStoreCalendarTypeUpdateCommand>
{
    public UpdateEmployeeStoreCalendarTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}