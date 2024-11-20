namespace Engage.Application.Services.PayrollYears.Commands;

public class PayrollYearInsertCommand : IMapTo<PayrollYear>, IRequest<PayrollYear>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollYearInsertCommand, PayrollYear>();
    }
}

public class PayrollYearInsertHandler: InsertHandler, IRequestHandler<PayrollYearInsertCommand, PayrollYear>
{
    public PayrollYearInsertHandler (IAppDbContext context, IMapper mapper) : base (context, mapper)
    {
    }

    public async Task<PayrollYear> Handle(PayrollYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PayrollYearInsertCommand, PayrollYear>(command);
        _context.PayrollYears.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryYearInsertValidator : AbstractValidator<PayrollYearInsertCommand>
{
    public InventoryYearInsertValidator ()
    {
        RuleFor(e => e.Name);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}