using Engage.Application.Services.StoreSurveys.Models;
using Engage.Application.Services.StoreSurveys.Queries;

namespace Engage.Application.Services.Mobile.Surveys.Queries;

public class GetSurveysByEmployeeStoreHistoryQuery : IRequest<List<StoreSurveysDto>>
{
    public int EmployeeId { get; set; }
    public DateTime TimezoneDate { get; set; }
}

public class GetSurveysByEmployeeStoreHistoryQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveysByEmployeeStoreHistoryQuery, List<StoreSurveysDto>>
{
    private IMediator _mediator;
    private IUserService _user;
    public GetSurveysByEmployeeStoreHistoryQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
    }

    public async Task<List<StoreSurveysDto>> Handle(GetSurveysByEmployeeStoreHistoryQuery request, CancellationToken cancellationToken)
    {
        var storeIds = new List<int>();

        var employeeStoreIds = await _context.EmployeeStores.Where(e => e.EmployeeId == request.EmployeeId).Select(s => s.StoreId).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeStoreIds);

        var employeeSurveyHistoryStoreIds = await _context.SurveyInstances.Where(e => e.EmployeeId == request.EmployeeId).Select(s => s.StoreId).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeSurveyHistoryStoreIds);

        var employeeOrderHistoryStoreIds = await _context.Orders.Where(e => e.CreatedBy == _user.UserName).Select(s => s.StoreId).ToListAsync(cancellationToken);

        storeIds.AddRange(employeeOrderHistoryStoreIds);

        storeIds = storeIds.Distinct().Order().ToList();

        List<StoreSurveysDto> storeSurveysList = new List<StoreSurveysDto>();

        foreach (var storeId in storeIds)
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

