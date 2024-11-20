namespace Engage.Application.Services.PaymentLineFiles.Commands;

public class PaymentLineFileInsertCommand : IMapTo<PaymentLineFile>, IRequest<PaymentLineFile>
{
    public int PaymentLineId { get; init; }
    public int PaymentLineFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileInsertCommand, PaymentLineFile>();
    }
}

public record PaymentLineFileInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileInsertCommand, PaymentLineFile>
{
    public async Task<PaymentLineFile> Handle(PaymentLineFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentLineFileInsertCommand, PaymentLineFile>(command);
        
        Context.PaymentLineFiles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentLineFileInsertValidator : AbstractValidator<PaymentLineFileInsertCommand>
{
    public PaymentLineFileInsertValidator()
    {
        RuleFor(e => e.PaymentLineId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentLineFileTypeId).NotEmpty().GreaterThan(0);
    }
}