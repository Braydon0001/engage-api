using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.StoreSurveys.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class GetStoreSurveys : GetQuery, IRequest<DataResult<StoreSurveysDto>>
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public DateTime TimezoneDate { get; set; }
    }

    public class GetStoreSurveysQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreSurveys, DataResult<StoreSurveysDto>>
    {
        public GetStoreSurveysQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<DataResult<StoreSurveysDto>> Handle(GetStoreSurveys request, CancellationToken cancellationToken)
        {
            var storeSurveys = await _context.SurveysByEmployeePerStoreView
                .Where(x =>
                       x.EmployeeId == request.EmployeeId &&
                       x.StoreId == request.StoreId &&
                       request.TimezoneDate >= x.Survey.StartDate &&
                       (x.Survey.EndDate.HasValue ? request.TimezoneDate <= x.Survey.EndDate : true) &&
                       // Exclude surveys that have already been completed by the employee for the store
                       // Recurring surveys: Completed once.    
                       // Non-recurring surveys: Completed on the specified date. 
                       !x.Survey.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == request.TimezoneDate) ||
                                                            e.Survey.IsRecurring == false) &&
                                                          e.EmployeeId == request.EmployeeId &&
                                                          e.StoreId == request.StoreId &&
                                                          e.SurveyId == x.SurveyId)
                       )
                .OrderBy(x => x.Survey.SupplierId)
                .ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            var regionSurveys = await _context.SurveysByEmployeePerRegionView2
                .Where(x =>
                       x.EmployeeId == request.EmployeeId &&
                       x.StoreId == request.StoreId &&
                       request.TimezoneDate >= x.Survey.StartDate &&
                       (x.Survey.EndDate.HasValue ? request.TimezoneDate <= x.Survey.EndDate : true) &&
                       // Exclude surveys that have already been completed by the employee for the store
                       // Recurring surveys: Completed once.    
                       // Non-recurring surveys: Completed on the specified date. 
                       !x.Survey.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == request.TimezoneDate) ||
                                                            e.Survey.IsRecurring == false) &&
                                                          e.EmployeeId == request.EmployeeId &&
                                                          e.StoreId == request.StoreId &&
                                                          e.SurveyId == x.SurveyId))
                .OrderBy(x => x.Survey.SupplierId)
                .ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            var surveys = storeSurveys.Concat(regionSurveys)
                .OrderBy(x=>x.SupplierId)
                .ToList();
            var transformedSurveys = StoreSurveyTransforms.GroupBySupplier(_mapper, surveys);

            return new DataResult<StoreSurveysDto>()
            {
                Data = transformedSurveys
            };  
        }
    }
}
