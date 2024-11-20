namespace Engage.Application.Services.PaymentBatches.Commands;

public class PaymentBatchUpdateCommand : IMapTo<PaymentBatch>, IRequest<PaymentBatch>
{
    public int Id { get; set; }
    public DateTime BatchDate { get; init; }
    public List<int> EngageRegionIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentBatchUpdateCommand, PaymentBatch>();
    }
}

public record PaymentBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentBatchUpdateCommand, PaymentBatch>
{
    public async Task<PaymentBatch> Handle(PaymentBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentBatches.SingleOrDefaultAsync(e => e.PaymentBatchId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        var existingRegions = await Context.PaymentBatchRegions.Where(e => e.PaymentBatchId == entity.PaymentBatchId)
                                                               .ToListAsync(cancellationToken);

        //get regions to delete and delete them
        var regionsToDelete = existingRegions.Where(e => !command.EngageRegionIds.Contains(e.EngageRegionId))
                                             .ToList();

        Context.PaymentBatchRegions.RemoveRange(regionsToDelete);

        //get regions to add and add them
        var existingRegionIds = existingRegions.Select(e => e.EngageRegionId).ToList();

        var regionIdsToAdd = command.EngageRegionIds.Where(id => !existingRegionIds.Contains(id)).ToList();

        foreach (var regionId in regionIdsToAdd)
        {
            Context.PaymentBatchRegions.Add(new PaymentBatchRegion
            {
                EngageRegionId = regionId,
                PaymentBatchId = entity.PaymentBatchId
            });
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentBatchValidator : AbstractValidator<PaymentBatchUpdateCommand>
{
    public UpdatePaymentBatchValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.BatchDate).NotEmpty();
        RuleFor(e => e.EngageRegionIds).NotEmpty();
    }
}