namespace Engage.Application.Services.PaymentLineFileTypes.Commands;

public class PaymentLineFileTypeUpdateCommand : IMapTo<PaymentLineFileType>, IRequest<PaymentLineFileType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLineFileTypeUpdateCommand, PaymentLineFileType>();
    }
}

public record PaymentLineFileTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileTypeUpdateCommand, PaymentLineFileType>
{
    public async Task<PaymentLineFileType> Handle(PaymentLineFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLineFileTypes.SingleOrDefaultAsync(e => e.PaymentLineFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentLineFileTypeValidator : AbstractValidator<PaymentLineFileTypeUpdateCommand>
{
    public UpdatePaymentLineFileTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}