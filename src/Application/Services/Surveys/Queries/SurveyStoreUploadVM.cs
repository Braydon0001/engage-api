using Engage.Application.Services.ImportFiles;
using Engage.Application.Services.Surveys.Models;

namespace Engage.Application.Services.Surveys.Queries;

public class SurveyStoreUploadVMQuery : GetByIdQuery, IRequest<ImportSurveyStoresVM>
{ }

public class SurveyStoreUploadVMQueryHandler : BaseViewModelQueryHandler, IRequestHandler<SurveyStoreUploadVMQuery, ImportSurveyStoresVM>
{
    private readonly IAppDbContext _context;

    public SurveyStoreUploadVMQueryHandler(IMediator mediator, IAppDbContext context) : base(mediator)
    {
        _context = context;
    }

    public async Task<ImportSurveyStoresVM> Handle(SurveyStoreUploadVMQuery request, CancellationToken cancellationToken)
    {
        var vm = new ImportSurveyStoresVM
        {
            Survey = await _mediator.Send(new SurveyQuery() { Id = request.Id }),
            StoreFormats = await _mediator.Send(new OptionsQuery(OptionDesc.STOREFORMATS))
        };

        var fileUploadIds = await _context.ImportSurveyStores
                                    .Where(e => e.SurveyId == request.Id)
                                    .Select(e => e.ImportFileId)
                                    .Distinct()
                                    .ToListAsync(cancellationToken);

        if (fileUploadIds != null && fileUploadIds.Count > 0)
        {
            var fileUploads = await _mediator.Send(new ImportFilesQuery
            {
                FileUploadIds = fileUploadIds,
                IsConfirmed = true
            });
            if (fileUploads.Data != null && fileUploads.Data.Count > 0)
            {
                vm.FileUploads = fileUploads.Data;
            }
        }

        return vm;
    }
}
