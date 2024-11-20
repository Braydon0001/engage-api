namespace Engage.Application.Services.SurveyFormProducts.Commands;

public class SurveyFormProductBatchUpdateCommand : IRequest<OperationStatus>
{
    public int SurveyFormId { get; init; }
    public List<int> EngageMasterProductIds { get; init; }
}

public record SurveyFormProductBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormProductBatchUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormProductBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        //get the current products
        var currentProductsIds = await Context.SurveyFormProducts.Where(e => e.SurveyFormId == command.SurveyFormId).Select(e => e.EngageMasterProductId).ToListAsync(cancellationToken);

        var productsToRemoveIds = new List<int>();

        if (currentProductsIds.Any() && (command.EngageMasterProductIds == null || !command.EngageMasterProductIds.Any()))
        {
            //get the products that should be removed
            productsToRemoveIds = currentProductsIds;
        }
        else if (command.EngageMasterProductIds != null && command.EngageMasterProductIds.Any())
        {
            //get the products that should be removed
            productsToRemoveIds = currentProductsIds.Except(command.EngageMasterProductIds).ToList();
        }

        if (productsToRemoveIds.Any())
        {
            var productsToRemove = await Context.SurveyFormProducts.Where(e => productsToRemoveIds.Contains(e.EngageMasterProductId)).ToListAsync(cancellationToken);

            //remove those products
            Context.SurveyFormProducts.RemoveRange(productsToRemove);
        }

        if (command.EngageMasterProductIds != null)
        {
            //get the products to add
            var productsToAddIds = command.EngageMasterProductIds.Except(currentProductsIds);

            if (productsToAddIds.Any())
            {
                //add the products
                foreach (var product in productsToAddIds)
                {
                    var entity = new SurveyFormProduct() { EngageMasterProductId = product, SurveyFormId = command.SurveyFormId };
                    Context.SurveyFormProducts.Add(entity);
                }

            }
        }

        //save
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class SurveyFormProductBatchUpdateValidator : AbstractValidator<SurveyFormProductBatchUpdateCommand>
{
    public SurveyFormProductBatchUpdateValidator()
    {
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
    }
}