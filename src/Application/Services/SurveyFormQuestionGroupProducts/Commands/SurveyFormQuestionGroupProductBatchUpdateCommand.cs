namespace Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

public class SurveyFormQuestionGroupProductBatchUpdateCommand : IRequest<OperationStatus>
{
    public int SurveyFormQuestionGroupId { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
}

public record SurveyFormQuestionGroupProductBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupProductBatchUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionGroupProductBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        //get the current products
        var currentProductsIds = await Context.SurveyFormQuestionGroupProducts.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId).Select(e => e.EngageVariantProductId).ToListAsync(cancellationToken);

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
            var productsToRemove = await Context.SurveyFormQuestionGroupProducts.Where(e => productsToRemoveIds.Contains(e.EngageVariantProductId)).ToListAsync(cancellationToken);

            //remove those products
            Context.SurveyFormQuestionGroupProducts.RemoveRange(productsToRemove);
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
                    var entity = new SurveyFormQuestionGroupProduct() { EngageVariantProductId = product, SurveyFormQuestionGroupId = command.SurveyFormQuestionGroupId };
                    Context.SurveyFormQuestionGroupProducts.Add(entity);
                }

            }
        }


        //save
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class SurveyFormQuestionGroupProductBatchUpdateValidator : AbstractValidator<SurveyFormQuestionGroupProductBatchUpdateCommand>
{
    public SurveyFormQuestionGroupProductBatchUpdateValidator()
    {
        RuleFor(e => e.SurveyFormQuestionGroupId).NotEmpty().GreaterThan(0);
    }
}