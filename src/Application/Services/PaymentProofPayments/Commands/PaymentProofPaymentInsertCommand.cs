namespace Engage.Application.Services.PaymentProofPayments.Commands;

public class PaymentProofPaymentInsertCommand : IMapTo<PaymentProofPayment>, IRequest<PaymentProofPayment>
{
    public int PaymentId { get; init; }
    public int PaymentProofId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofPaymentInsertCommand, PaymentProofPayment>();
    }
}

public record PaymentProofPaymentInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofPaymentInsertCommand, PaymentProofPayment>
{
    public async Task<PaymentProofPayment> Handle(PaymentProofPaymentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentProofPaymentInsertCommand, PaymentProofPayment>(command);
        
        Context.PaymentProofPayments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentProofPaymentInsertValidator : AbstractValidator<PaymentProofPaymentInsertCommand>
{
    public PaymentProofPaymentInsertValidator()
    {
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentProofId).NotEmpty().GreaterThan(0);
    }
}