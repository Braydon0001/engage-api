namespace Engage.Application.Services.SupplierPeriods.Commands;

public class SupplierPeriodUpdateCommand : IMapTo<SupplierPeriod>, IRequest<SupplierPeriod>
{
    public int Id { get; set; }
    public int SupplierYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierPeriodUpdateCommand, SupplierPeriod>();
    }

}

public class SupplierPeriodUpdateHandler : UpdateHandler, IRequestHandler<SupplierPeriodUpdateCommand, SupplierPeriod>
{
    public SupplierPeriodUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierPeriod> Handle(SupplierPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.SupplierPeriods.SingleOrDefaultAsync(e => e.SupplierPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var previousPeriods = await _context.SupplierPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.SupplierPeriodId != entity.SupplierPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.SupplierPeriodId != entity.SupplierPeriodId
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

public class SupplierPeriodUpdateValidator : AbstractValidator<SupplierPeriodUpdateCommand>
{
    public SupplierPeriodUpdateValidator()
    {
        RuleFor(e => e.SupplierYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Number).NotEmpty();
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}
