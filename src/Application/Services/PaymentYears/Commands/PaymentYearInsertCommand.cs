namespace Engage.Application.Services.PaymentYears.Commands;

public class PaymentYearInsertCommand : IMapTo<PaymentYear>, IRequest<PaymentYear>
{
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentYearInsertCommand, PaymentYear>();
    }
}

public record PaymentYearInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentYearInsertCommand, PaymentYear>
{
    public async Task<PaymentYear> Handle(PaymentYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentYearInsertCommand, PaymentYear>(command);
        
        Context.PaymentYears.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentYearInsertValidator : AbstractValidator<PaymentYearInsertCommand>
{
    public PaymentYearInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}