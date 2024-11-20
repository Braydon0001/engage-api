namespace Engage.Application.Services.PaymentStatuses.Commands;

public class PaymentStatusInsertCommand : IMapTo<PaymentStatus>, IRequest<PaymentStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusInsertCommand, PaymentStatus>();
    }
}

public record PaymentStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusInsertCommand, PaymentStatus>
{
    public async Task<PaymentStatus> Handle(PaymentStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentStatusInsertCommand, PaymentStatus>(command);
        
        Context.PaymentStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentStatusInsertValidator : AbstractValidator<PaymentStatusInsertCommand>
{
    public PaymentStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}