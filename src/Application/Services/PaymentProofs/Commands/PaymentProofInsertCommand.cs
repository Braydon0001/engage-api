namespace Engage.Application.Services.PaymentProofs.Commands;

public class PaymentProofInsertCommand : IMapTo<PaymentProof>, IRequest<PaymentProof>
{

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentProofInsertCommand, PaymentProof>();
    }
}

public record PaymentProofInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofInsertCommand, PaymentProof>
{
    public async Task<PaymentProof> Handle(PaymentProofInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentProofInsertCommand, PaymentProof>(command);
        
        Context.PaymentProofs.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentProofInsertValidator : AbstractValidator<PaymentProofInsertCommand>
{
    public PaymentProofInsertValidator()
    {

    }
}