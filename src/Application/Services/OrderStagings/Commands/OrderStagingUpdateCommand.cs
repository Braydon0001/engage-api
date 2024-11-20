namespace Engage.Application.Services.OrderStagings.Commands;

public class OrderStagingUpdateCommand : IMapTo<OrderStaging>, IRequest<OrderStaging>
{
    public int Id { get; set; }
    public string Region { get; init; }
    public string StoreName { get; init; }
    public string AccountNumber { get; init; }
    public string OrderNumber { get; init; }
    public string OrderContactName { get; init; }
    public string OrderContactEmail { get; init; }
    public string VatNumber { get; init; }
    public string OrderDate { get; init; }
    public string Reference { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingUpdateCommand, OrderStaging>()
            .ForMember(d => d.Date, opt => opt.MapFrom(s => s.OrderDate));
    }
}

public record OrderStagingUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingUpdateCommand, OrderStaging>
{
    public async Task<OrderStaging> Handle(OrderStagingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.OrderStagings.SingleOrDefaultAsync(e => e.OrderStagingId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrderStagingValidator : AbstractValidator<OrderStagingUpdateCommand>
{
    public UpdateOrderStagingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Region).MaximumLength(120);
        RuleFor(e => e.StoreName).MaximumLength(120);
        RuleFor(e => e.AccountNumber).MaximumLength(120);
        RuleFor(e => e.OrderNumber).MaximumLength(120);
        RuleFor(e => e.OrderContactName).MaximumLength(120);
        RuleFor(e => e.OrderContactEmail).MaximumLength(120);
        RuleFor(e => e.VatNumber).MaximumLength(120);
    }
}