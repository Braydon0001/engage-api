namespace Engage.Application.Services.PaymentPeriods.Commands;

public class PaymentPeriodInsertCommand : IMapTo<PaymentPeriod>, IRequest<PaymentPeriod>
{
    public int PaymentYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentPeriodInsertCommand, PaymentPeriod>();
    }
}

public record PaymentPeriodInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentPeriodInsertCommand, PaymentPeriod>
{
    public async Task<PaymentPeriod> Handle(PaymentPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentPeriodInsertCommand, PaymentPeriod>(command);
        
        Context.PaymentPeriods.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentPeriodInsertValidator : AbstractValidator<PaymentPeriodInsertCommand>
{
    public PaymentPeriodInsertValidator()
    {
        RuleFor(e => e.PaymentYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}