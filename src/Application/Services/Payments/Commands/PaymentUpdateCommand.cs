namespace Engage.Application.Services.Payments.Commands;

public class PaymentUpdateCommand : IMapTo<Payment>, IRequest<Payment>
{
    public int Id { get; set; }
    public int EngageRegionId { get; init; }
    public int CreditorId { get; init; }
    //public int PaymentStatusId { get; init; }
    public int PaymentCostTypeId { get; init; }
    public int VatId { get; init; }
    //public int PaymentPeriodId { get; init; }
    public string InvoiceNumber { get; init; }
    public DateTime InvoiceDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentUpdateCommand, Payment>();
    }
}

public record PaymentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentUpdateCommand, Payment>
{
    public async Task<Payment> Handle(PaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Payments.SingleOrDefaultAsync(e => e.PaymentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentValidator : AbstractValidator<PaymentUpdateCommand>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageRegionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.PaymentStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentCostTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.VatId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.PaymentPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InvoiceNumber).NotEmpty().MaximumLength(100);
        RuleFor(e => e.InvoiceDate).NotEmpty();
    }
}