namespace Engage.Application.Services.SurveyFormQuestionProducts.Commands;

public class SurveyFormQuestionProductBatchUpdateCommand : IRequest<OperationStatus>
{
    public int SurveyFormQuestionId { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
}

public record SurveyFormQuestionProductBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionProductBatchUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionProductBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        //get the current products
        var currentProductsIds = await Context.SurveyFormQuestionProducts.Where(e => e.SurveyFormQuestionId == command.SurveyFormQuestionId).Select(e => e.EngageVariantProductId).ToListAsync(cancellationToken);

        var productsToRemoveIds = new List<int>();

        if (currentProductsIds.Any() && (command.EngageVariantProductIds == null || !command.EngageVariantProductIds.Any()))
        {
            //get the products that should be removed
            productsToRemoveIds = currentProductsIds;
        }
        else if (command.EngageVariantProductIds != null && command.EngageVariantProductIds.Any())
        {
            //get the products that should be removed
            productsToRemoveIds = currentProductsIds.Except(command.EngageVariantProductIds).ToList();
        }

        if (productsToRemoveIds.Any())
        {
            var productsToRemove = await Context.SurveyFormQuestionProducts.Where(e => productsToRemoveIds.Contains(e.EngageVariantProductId)).ToListAsync(cancellationToken);

            //remove those products
            Context.SurveyFormQuestionProducts.RemoveRange(productsToRemove);
        }

        if (command.EngageVariantProductIds != null)
        {
            //get the products to add
            var productsToAddIds = command.EngageVariantProductIds.Except(currentProductsIds);

            if (productsToAddIds.Any())
            {
                //add the products
                foreach (var product in productsToAddIds)
                {
                    var entity = new SurveyFormQuestionProduct() { EngageVariantProductId = product, SurveyFormQuestionId = command.SurveyFormQuestionId };
                    Context.SurveyFormQuestionProducts.Add(entity);
                }

            }
        }


        //save
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class SurveyFormQuestionProductBatchUpdateValidator : AbstractValidator<SurveyFormQuestionProductBatchUpdateCommand>
{
    public SurveyFormQuestionProductBatchUpdateValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
    }
}