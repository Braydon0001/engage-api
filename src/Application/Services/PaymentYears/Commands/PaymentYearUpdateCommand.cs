namespace Engage.Application.Services.PaymentYears.Commands;

public class PaymentYearUpdateCommand : IMapTo<PaymentYear>, IRequest<PaymentYear>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentYearUpdateCommand, PaymentYear>();
    }
}

public record PaymentYearUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentYearUpdateCommand, PaymentYear>
{
    public async Task<PaymentYear> Handle(PaymentYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentYears.SingleOrDefaultAsync(e => e.PaymentYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentYearValidator : AbstractValidator<PaymentYearUpdateCommand>
{
    public UpdatePaymentYearValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}