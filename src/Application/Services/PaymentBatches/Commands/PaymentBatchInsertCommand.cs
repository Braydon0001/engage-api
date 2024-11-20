namespace Engage.Application.Services.PaymentBatches.Commands;

public class PaymentBatchInsertCommand : IMapTo<PaymentBatch>, IRequest<PaymentBatch>
{
    public DateTime BatchDate { get; init; }
    public List<int> EngageRegionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentBatchInsertCommand, PaymentBatch>();
    }
}

public record PaymentBatchInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentBatchInsertCommand, PaymentBatch>
{
    public async Task<PaymentBatch> Handle(PaymentBatchInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentBatchInsertCommand, PaymentBatch>(command);

        foreach (var regionId in command.EngageRegionIds)
        {
            var newRegionBatch = new PaymentBatchRegion
            {
                PaymentBatch = entity,
                EngageRegionId = regionId,
            };

            entity.BatchRegions.Add(newRegionBatch);
        }

        Context.PaymentBatches.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class PaymentBatchInsertValidator : AbstractValidator<PaymentBatchInsertCommand>
{
    public PaymentBatchInsertValidator()
    {
        RuleFor(e => e.BatchDate).NotEmpty();
        RuleFor(e => e.EngageRegionIds).NotEmpty();
    }
}