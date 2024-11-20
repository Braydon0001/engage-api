// auto-generated
namespace Engage.Application.Services.ProductPeriods.Commands;

public class ProductPeriodUpdateCommand : IMapTo<ProductPeriod>, IRequest<ProductPeriod>
{
    public int Id { get; set; }
    public int ProductYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPeriodUpdateCommand, ProductPeriod>();
    }
}

public class ProductPeriodUpdateHandler : UpdateHandler, IRequestHandler<ProductPeriodUpdateCommand, ProductPeriod>
{
    public ProductPeriodUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductPeriod> Handle(ProductPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.ProductPeriods.SingleOrDefaultAsync(e => e.ProductPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var previousPeriods = await _context.ProductPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.ProductPeriodId != entity.ProductPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.ProductPeriodId != entity.ProductPeriodId
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

public class UpdateProductPeriodValidator : AbstractValidator<ProductPeriodUpdateCommand>
{
    public UpdateProductPeriodValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}