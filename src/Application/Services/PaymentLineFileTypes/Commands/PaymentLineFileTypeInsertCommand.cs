namespace Engage.Application.Services.PaymentLineFileTypes.Commands;

public class PaymentLineFileTypeInsertCommand : IMapTo<PaymentLineFileType>, IRequest<PaymentLineFileType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileTypeInsertCommand, PaymentLineFileType>();
    }
}

public record PaymentLineFileTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileTypeInsertCommand, PaymentLineFileType>
{
    public async Task<PaymentLineFileType> Handle(PaymentLineFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentLineFileTypeInsertCommand, PaymentLineFileType>(command);
        
        Context.PaymentLineFileTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentLineFileTypeInsertValidator : AbstractValidator<PaymentLineFileTypeInsertCommand>
{
    public PaymentLineFileTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}