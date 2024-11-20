using Engage.Application.Services.ImportFiles;
using Microsoft.AspNetCore.Http;

namespace Engage.Application.Services.ImportSurveyStores
{
    // Commands    
    public class ImportSurveyStoresCommand : IRequest<OperationStatus>
    {
        public int SurveyId { get; set; }
        public List<int> StoreFormatIds { get; set; }
        public IFormFile File { get; set; }
    }

    public class ProcessSurveyStoresImportCommand : IRequest<OperationStatus>
    {
        public int ImportFileId { get; set; }
        public int SurveyId { get; set; }
    }

    // Handlers
    public class ImportSurveyStoresCommandHandler : BaseCreateCommandHandler, IRequestHandler<ImportSurveyStoresCommand, OperationStatus>
    {
        private readonly IOptions<ImportFileOptions> _importFileOptions;
        private readonly ICsvService _csv;

        public ImportSurveyStoresCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<ImportFileOptions> importFileOptions, ICsvService csvService) : base(context, mapper, mediator)
        {
            _importFileOptions = importFileOptions;
            _csv = csvService;
        }

        public async Task<OperationStatus> Handle(ImportSurveyStoresCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var options = new ReadFileOptions
                {
                    File = command.File,
                    Folder = _importFileOptions.Value.SurveyStoresFolder,
                    MaxRows = _importFileOptions.Value.SurveyStoresMaxRows,
                };

                // Read the csv file into a collection
                var readFileResult = await _csv.ReadFile<AccountNoImport>(options, cancellationToken);

                // Create a file upload record
                var status = await _mediator.Send(new CreateImportFileCommand
                {
                    FileName = readFileResult.FileName
                });
                var importFileId = Convert.ToInt32(status.OperationId);

                // Create the import entities from the collection, and insert them into the context
                var accountNos = readFileResult.Data.Select(e => e.AccountNo).ToList();
                var importEntities = await CreateImportSurveyStores(importFileId, command.SurveyId, command.StoreFormatIds, accountNos, _context, cancellationToken);
                _context.ImportSurveyStores.AddRange(importEntities);

                // Save the changes 
                var opStatus = await _context.SaveChangesAsync(cancellationToken);

                //Return the file import id and import file Result  
                opStatus.OperationId = importFileId;
                var imports = await _mediator.Send(new ImportSurveyStoresQuery
                {
                    ImportFileId = importFileId,
                    SurveyId = command.SurveyId
                });
                var data = imports.Data;
                opStatus.ReturnObject = new ImportFileResult<ImportSurveyStoreListDto>
                {
                    Data = data,
                    FileName = readFileResult.FileName,
                    SuccessCount = data.Where(e => e.ImportRowType == ImportRowType.Success).Count(),
                    ErrorCount = data.Where(e => e.ImportRowType == ImportRowType.Error).Count(),
                    WarningCount = data.Where(e => e.ImportRowType == ImportRowType.Warning).Count()
                };
                return opStatus;
            }
            catch (ImportFileException ex)
            {
                return OperationStatus.CreateFromException("Import Stores Error", ex);
            }
        }

        private static async Task<IEnumerable<ImportSurveyStore>> CreateImportSurveyStores(int importFileId, int surveyId, List<int> storeFormatIds, List<string> accountNos, IAppDbContext context, CancellationToken cancellationToken)
        {
            var allDcAccounts = await context.DCAccounts
                                        .Include(e => e.Store)
                                        .Where(e => accountNos.Contains(e.AccountNumber) &&
                                                    storeFormatIds.Contains(e.Store.StoreFormatId))
                                        .Select(e => new
                                        {
                                            e.Store,
                                            e.AccountNumber
                                        })
                                        .ToListAsync(cancellationToken);

            var missingAccountNumbers = await context.DCAccounts.Where(e => !accountNos.Contains(e.AccountNumber))
                                                                .Select(e => e.AccountNumber)
                                                                .ToListAsync(cancellationToken);

            var existingSurveyStores = await context.SurveyStores
                                    .Where(e => e.SurveyId == surveyId)
                                    .Select(e => e.StoreId)
                                    .ToListAsync(cancellationToken);

            var imports = new List<ImportSurveyStore>();
            var rowNo = 1;
            foreach (var accountNo in accountNos)
            {
                if (missingAccountNumbers.Count > 0 && missingAccountNumbers.Contains(accountNo))
                {
                    imports.Add(new ImportSurveyStore
                    {
                        ImportFileId = importFileId,
                        SurveyId = surveyId,
                        RowNo = rowNo++,
                        ImportRowType = ImportRowType.Error,
                        ImportRowMessage = $"There are no stores with account no: {accountNo}",
                        AccountNumber = accountNo,
                    });
                }
                else
                {
                    var dcAccounts = allDcAccounts.Where(e => e.AccountNumber == accountNo).ToList();

                    foreach (var dcAccount in dcAccounts)
                    {
                        var import = new ImportSurveyStore
                        {
                            ImportFileId = importFileId,
                            SurveyId = surveyId,
                            RowNo = rowNo++,
                            StoreId = dcAccount.Store.StoreId,
                            AccountNumber = accountNo,
                        };

                        // Errors
                        if (existingSurveyStores.Count > 0 && existingSurveyStores.Contains(dcAccount.Store.StoreId))
                        {
                            import.ImportRowType = ImportRowType.Error;
                            import.ImportRowMessage = $"This store has already been imported";
                        }
                        // Warnings                        
                        else if (imports.Where(e => e.StoreId == dcAccount.Store.StoreId).Any())
                        {
                            import.ImportRowType = ImportRowType.Warning;
                            import.ImportRowMessage = $"There is already a row for this store";
                        }
                        else if (dcAccount.Store.Disabled)
                        {
                            import.ImportRowType = ImportRowType.Warning;
                            import.ImportRowMessage = $"This store is INACTIVE";
                        }
                        else
                        {
                            import.ImportRowType = ImportRowType.Success;
                        }

                        imports.Add(import);
                    }
                }
            }
            return imports;
        }
    }

    public class ProcessSurveyStoresImportCommandHandler : BaseCreateCommandHandler, IRequestHandler<ProcessSurveyStoresImportCommand, OperationStatus>
    {
        public ProcessSurveyStoresImportCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(ProcessSurveyStoresImportCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Validation
                var imports = await _context.ImportSurveyStores
                                            .Where(e => e.ImportFileId == command.ImportFileId &&
                                                        e.SurveyId == command.SurveyId)
                                            .ToListAsync(cancellationToken);

                var errors = imports.Where(e => e.ImportRowType == ImportRowType.Error).Count();
                var warnings = imports.Where(e => e.ImportRowType == ImportRowType.Warning).Count();

                FileUtils.ValidateImportedFileCounts(imports.Count, errors, warnings);


                // Insert with select from import table 
                var sql = "INSERT INTO SurveyStores (SurveyId, StoreId) " +
                          "SELECT DISTINCT SurveyId, StoreId FROM ImportSurveyStores WHERE ImportRowType = 1 AND ImportFileId = {0} AND SurveyId = {1};" +
                          "SELECT * FROM SurveyStores WHERE SurveyId = {1};";
                await _context.SurveyStores.FromSqlRaw(sql, command.ImportFileId, command.SurveyId)
                                           .ToListAsync(cancellationToken);

                // Set the date that the upload was confirmed
                var opStatus = await _mediator.Send(new UpdateImportFileCommand
                {
                    Id = command.ImportFileId,
                    ConfirmedDate = DateTime.Now
                });

                return opStatus;
            }
            catch (ImportFileException ex)
            {
                return OperationStatus.CreateFromException("Import Stores Error", ex);
            }
        }
    }
}
