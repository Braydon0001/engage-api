namespace Engage.Application.Services.SupplierPeriods.Commands;

public class SupplierPeriodInsertCommand : IMapTo<SupplierPeriod>, IRequest<SupplierPeriod>
{
    public int SupplierYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierPeriodInsertCommand, SupplierPeriod>();
    }
}

public class SupplierPeriodInsertHandler : InsertHandler, IRequestHandler<SupplierPeriodInsertCommand, SupplierPeriod>
{
    public SupplierPeriodInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierPeriod> Handle(SupplierPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.SupplierPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var entity = _mapper.Map<SupplierPeriodInsertCommand, SupplierPeriod>(command);

        _context.SupplierPeriods.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

}

public class SupplierPeriodInsertValidator : AbstractValidator<SupplierPeriodInsertCommand>
{
    public SupplierPeriodInsertValidator()
    {
        RuleFor(e => e.SupplierYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Number).NotEmpty();
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}
