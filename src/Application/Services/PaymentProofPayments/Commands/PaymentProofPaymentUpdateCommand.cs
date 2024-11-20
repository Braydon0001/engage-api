namespace Engage.Application.Services.PaymentProofPayments.Commands;

public class PaymentProofPaymentUpdateCommand : IMapTo<PaymentProofPayment>, IRequest<PaymentProofPayment>
{
    public int Id { get; set; }
    public int PaymentId { get; init; }
    public int PaymentProofId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofPaymentUpdateCommand, PaymentProofPayment>();
    }
}

public record PaymentProofPaymentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofPaymentUpdateCommand, PaymentProofPayment>
{
    public async Task<PaymentProofPayment> Handle(PaymentProofPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentProofPayments.SingleOrDefaultAsync(e => e.PaymentProofPaymentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentProofPaymentValidator : AbstractValidator<PaymentProofPaymentUpdateCommand>
{
    public UpdatePaymentProofPaymentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentProofId).NotEmpty().GreaterThan(0);
    }
}