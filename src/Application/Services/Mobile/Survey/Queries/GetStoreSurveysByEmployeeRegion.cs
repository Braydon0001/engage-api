using Engage.Application.Services.StoreSurveys.Models;
using Engage.Application.Services.StoreSurveys.Queries;

namespace Engage.Application.Services.Mobile.Surveys.Queries;

public class GetStoreSurveysByEmployeeRegionQuery : IRequest<List<StoreSurveysDto>>
{
    public int EmployeeId { get; set; }
    public DateTime TimezoneDate { get; set; }
}

public class GetStoreSurveysByEmployeeRegionQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreSurveysByEmployeeRegionQuery, List<StoreSurveysDto>>
{
    private IMediator _mediator;
    public GetStoreSurveysByEmployeeRegionQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<StoreSurveysDto>> Handle(GetStoreSurveysByEmployeeRegionQuery request, CancellationToken cancellationToken)
    {
        //var employeeStores = await _mediator.Send(new GetStoreByEmployeeRegionQuery()
        //{
        //    EmployeeId = request.EmployeeId,
        //});

        var employeeRegions = await _context.EmployeeRegions
                                        .Where(e => e.EmployeeId == request.EmployeeId)
                                        .Select(s => s.EngageRegionId)
                                        .ToListAsync(cancellationToken);

        var employeeRegionStoreIds = await _context.Stores.Where(e => employeeRegions.Contains(e.EngageRegionId)).Select(e => e.StoreId).ToListAsync(cancellationToken);

        var uniqueStores = employeeRegionStoreIds.Distinct().Order().ToList();

        List<StoreSurveysDto> storeSurveysList = new List<StoreSurveysDto>();

        foreach (var storeId in employeeRegionStoreIds)
        {

            var storeSurveys = await _mediator.Send(new GetTargetedSurveys2()
            {
                EmployeeId = request.EmployeeId,
                StoreId = storeId,
                TimezoneDate = request.TimezoneDate,
            });

            if (storeSurveys.Data.SuppliersCount > 0)
            {
                storeSurveysList.Add(storeSurveys.Data);
            }


        }

        return storeSurveysList;
    }
}

