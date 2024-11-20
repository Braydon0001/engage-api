
namespace Engage.Application.Services.SubContractorBrands.Commands;

public class SubContractorBrandInsertCommand : IRequest<OperationStatus>
{
    public int ParentId { get; set; }
    public int SupplierId { get; set; }
    public List<int> EngageBrandIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
}

public record SubContractorBrandInsertHandler(IMediator Mediator, IAppDbContext Context) : IRequestHandler<SubContractorBrandInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SubContractorBrandInsertCommand command, CancellationToken cancellationToken)
    {
        var supplierSubContracts = await Context.SubContractorBrands.Where(e => e.SupplierId == command.SupplierId).ToListAsync(cancellationToken);

        List<SubContractorBrand> subContracts = [];
        foreach (var brand in command.EngageBrandIds)
        {
            foreach (var region in command.EngageRegionIds)
            {
                var existingSubContract = supplierSubContracts.Where(e => e.EngageBrandId == brand && e.EngageRegionId == region).FirstOrDefault();
                if (existingSubContract == null)
                {
                    subContracts.Add(new SubContractorBrand
                    {
                        ParentId = command.ParentId,
                        SupplierId = command.SupplierId,
                        EngageBrandId = brand,
                        EngageRegionId = region,
                    });
                }
            }
        }

        Context.SubContractorBrands.AddRange(subContracts);

        var operationStatus = await Context.SaveChangesAsync(cancellationToken);

        return operationStatus;
    }
}

public class SubContractorBrandInsertValidator : AbstractValidator<SubContractorBrandInsertCommand>
{
    public SubContractorBrandInsertValidator()
    {
        RuleFor(e => e.ParentId).NotNull().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotNull().GreaterThan(0);
        RuleForEach(e => e.EngageBrandIds).GreaterThan(0);
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
    }
}