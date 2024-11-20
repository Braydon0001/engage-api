namespace Engage.Application.Services.PaymentProofs.Commands;

public class PaymentProofUpdateCommand : IMapTo<PaymentProof>, IRequest<PaymentProof>
{
    public int Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofUpdateCommand, PaymentProof>();
    }
}

public record PaymentProofUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofUpdateCommand, PaymentProof>
{
    public async Task<PaymentProof> Handle(PaymentProofUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentProofs.SingleOrDefaultAsync(e => e.PaymentProofId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentProofValidator : AbstractValidator<PaymentProofUpdateCommand>
{
    public UpdatePaymentProofValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);

    }
}