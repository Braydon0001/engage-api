namespace Engage.Application.Services.PaymentPeriods.Commands;

public class PaymentPeriodUpdateCommand : IMapTo<PaymentPeriod>, IRequest<PaymentPeriod>
{
    public int Id { get; set; }
    public int PaymentYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentPeriodUpdateCommand, PaymentPeriod>();
    }
}

public record PaymentPeriodUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentPeriodUpdateCommand, PaymentPeriod>
{
    public async Task<PaymentPeriod> Handle(PaymentPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentPeriods.SingleOrDefaultAsync(e => e.PaymentPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentPeriodValidator : AbstractValidator<PaymentPeriodUpdateCommand>
{
    public UpdatePaymentPeriodValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}