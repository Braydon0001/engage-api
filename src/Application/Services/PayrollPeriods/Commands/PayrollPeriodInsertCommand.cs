
namespace Engage.Application.Services.PayrollPeriods.Commands;

public class PayrollPeriodInsertCommand : IMapTo<PayrollPeriod>, IRequest<PayrollPeriod>
{
    public int PayrollYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollPeriodInsertCommand, PayrollPeriod>();
    }
}

public class PayrollPeriodInsertCommandHandler : InsertHandler, IRequestHandler<PayrollPeriodInsertCommand, PayrollPeriod>
{
    public PayrollPeriodInsertCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PayrollPeriod> Handle(PayrollPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.PayrollPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);

        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var entity = _mapper.Map<PayrollPeriodInsertCommand, PayrollPeriod>(command);
        _context.PayrollPeriods.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PayrollPeriodInsertValidator : AbstractValidator<PayrollPeriodInsertCommand>
{
    public PayrollPeriodInsertValidator()
    {
        RuleFor(e => e.PayrollYearId).GreaterThan(0);
        RuleFor(e => e.Name);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}