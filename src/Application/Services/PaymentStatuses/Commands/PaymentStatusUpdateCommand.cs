namespace Engage.Application.Services.PaymentStatuses.Commands;

public class PaymentStatusUpdateCommand : IMapTo<PaymentStatus>, IRequest<PaymentStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusUpdateCommand, PaymentStatus>();
    }
}

public record PaymentStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusUpdateCommand, PaymentStatus>
{
    public async Task<PaymentStatus> Handle(PaymentStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentStatuses.SingleOrDefaultAsync(e => e.PaymentStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentStatusValidator : AbstractValidator<PaymentStatusUpdateCommand>
{
    public UpdatePaymentStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}