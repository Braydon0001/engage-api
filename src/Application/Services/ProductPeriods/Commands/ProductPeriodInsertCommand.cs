// auto-generated
namespace Engage.Application.Services.ProductPeriods.Commands;

public class ProductPeriodInsertCommand : IMapTo<ProductPeriod>, IRequest<ProductPeriod>
{
    public int ProductYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPeriodInsertCommand, ProductPeriod>();
    }
}

public class ProductPeriodInsertHandler : InsertHandler, IRequestHandler<ProductPeriodInsertCommand, ProductPeriod>
{
    public ProductPeriodInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPeriod> Handle(ProductPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.ProductPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var entity = _mapper.Map<ProductPeriodInsertCommand, ProductPeriod>(command);

        _context.ProductPeriods.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductPeriodInsertValidator : AbstractValidator<ProductPeriodInsertCommand>
{
    public ProductPeriodInsertValidator()
    {
        RuleFor(e => e.ProductYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}