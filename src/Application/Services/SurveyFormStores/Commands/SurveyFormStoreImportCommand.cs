namespace Engage.Application.Services.SurveyFormStores.Commands;

public class SurveyFormStoreImportCommand : IRequest<List<int>>
{
    public int SurveyFormId { get; set; }
    public List<SurveyFormStoreImport> Stores { get; set; }
}

public record SurveyFormStoreImportHandler(IMediator Mediator, IAppDbContext Context, ICsvService CsvService) : IRequestHandler<SurveyFormStoreImportCommand, List<int>>
{
    public async Task<List<int>> Handle(SurveyFormStoreImportCommand command, CancellationToken cancellationToken)
    {
        var importedStores = command.Stores.Select(e => e.StoreId).ToList();
        //var importedStoreAccountNos = command.Stores.Select(e => e.AccountNo.ToString()).ToList();

        var storeIds = await Context.Stores.Where(e => importedStores.Contains(e.StoreId))
                                           .Select(e => e.StoreId)
                                           .Distinct()
                                           .ToListAsync(cancellationToken);

        //var storeIds = await Context.DCAccounts.Include(e => e.Store)
        //                                       .ThenInclude(e => e.StoreFormat)
        //                                       .Where(e => importedStoreAccountNos.Contains(e.AccountNumber) && e.Store.StoreFormat.Name.ToLower() == "tops")
        //                                       .Select(e => e.StoreId)
        //                                       .Distinct()
        //                                       .ToListAsync(cancellationToken);

        return await Mediator.Send(new SurveyFormStoreBulkInsertCommand(command.SurveyFormId, storeIds), cancellationToken);

    }
}
