

namespace Engage.Application.Services.PayrollPeriods.Commands;

public class PayrollPeriodUpdateCommand : IMapTo<PayrollPeriod>, IRequest<PayrollPeriod>
{
    public int Id { get; set; }
    public int PayrollYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollPeriodUpdateCommand, PayrollPeriod>();
    }
}

public class PayrollPeriodUpdateCommandHandler : UpdateHandler, IRequestHandler<PayrollPeriodUpdateCommand, PayrollPeriod>
{
    public PayrollPeriodUpdateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PayrollPeriod> Handle(PayrollPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.PayrollPeriods.SingleOrDefaultAsync(e => e.PayrollPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var previousPeriods = await _context.PayrollPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.PayrollPeriodId != entity.PayrollPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.PayrollPeriodId != entity.PayrollPeriodId
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}
public class UpdatePayrollPeriodValidator : AbstractValidator<PayrollPeriodUpdateCommand>
{
    public UpdatePayrollPeriodValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.PayrollYearId).GreaterThan(0);
        RuleFor(e => e.Name);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}