namespace Engage.Application.Services.Payments.Commands;

public class PaymentInsertCommand : IMapTo<Payment>, IRequest<Payment>
{
    public int PaymentBatchId { get; set; }
    public int CreditorId { get; init; }
    public string InvoiceNumber { get; init; }
    public DateTime InvoiceDate { get; init; }
    public bool IsClaimFromSupplier { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentInsertCommand, Payment>();
    }
}

public record PaymentInsertHandler(IAppDbContext Context, IMapper Mapper, IFileService File) : IRequestHandler<PaymentInsertCommand, Payment>
{
    public async Task<Payment> Handle(PaymentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentInsertCommand, Payment>(command);

        entity.PaymentStatusId = (int)PaymentStatusId.New;

        var paymentPeriod = await Context.PaymentPeriods.SingleOrDefaultAsync(e => DateTime.Now.Date >= e.StartDate.Date &&
                                                                                   DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (paymentPeriod == null)
        {
            throw new Exception("There is no Payment Period for today's date");
        }

        entity.PaymentPeriodId = paymentPeriod.PaymentPeriodId;

        Context.Payments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentInsertValidator : AbstractValidator<PaymentInsertCommand>
{
    public PaymentInsertValidator()
    {
        RuleFor(e => e.PaymentBatchId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InvoiceNumber).NotEmpty().MaximumLength(100);
        RuleFor(e => e.InvoiceDate).NotEmpty();
    }
}