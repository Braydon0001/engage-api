using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.StoreSurveys.Models;
using Engage.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class GetSurveysByEmployeeQuery : GetQuery, IRequest<DataResult<StoreSurveysDto>>
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public DateTime TimezoneDate { get; set; }
    }

    public class GetSurveysByEmployeeQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveysByEmployeeQuery, DataResult<StoreSurveysDto>>
    {
        public GetSurveysByEmployeeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<DataResult<StoreSurveysDto>> Handle(GetSurveysByEmployeeQuery query, CancellationToken token)
        {
            var surveys = new List<StoreSurveyResult>();

            var regionsurveys = await _context.SurveysByEmployeePerRegionView.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);
            var storeSurveys = await _context.SurveysByEmployeePerStoreView_.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);
            var storeFormatSurveys = await _context.SurveysByEmployeePerStoreFormatView.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);
            var regionSurveys2 = await _context.SurveysByEmployeeSubGroupPerRegionView.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);
            var storeSurveys2 = await _context.SurveysByEmployeeSubGroupPerStoreView.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);
            var storeFormatSurveys2 = await _context.SurveysByEmployeeSubGroupPerStoreFormatView.WhereSurveys(query).ProjectTo<StoreSurveyResult>(_mapper.ConfigurationProvider).ToListAsync(token);

            if (regionsurveys.Count > 0) { surveys = surveys.Concat(regionsurveys).ToList(); }
            if (storeSurveys.Count > 0) { surveys = surveys.Concat(storeSurveys).ToList(); }
            if (storeFormatSurveys.Count > 0) { surveys = surveys.Concat(storeFormatSurveys).ToList(); }
            if (regionSurveys2.Count > 0) { surveys = surveys.Concat(regionSurveys2).ToList(); }
            if (storeSurveys2.Count > 0) { surveys = surveys.Concat(storeSurveys2).ToList(); }
            if (storeFormatSurveys2.Count > 0) { surveys = surveys.Concat(storeFormatSurveys2).ToList(); }

            var transformedSurveys = StoreSurveyTransforms.GroupBySupplier(_mapper, surveys);

            return new DataResult<StoreSurveysDto>()
            {
                Data = transformedSurveys
            };
        }
    }

    public static class SurveysByEmployeeExtensions
    {
        public static IQueryable<T> WhereSurveys<T>(this IQueryable<T> queryable, GetSurveysByEmployeeQuery query) where T : SurveysByEmployeeView
        {
            return queryable.Where(x =>
                                   x.EmployeeId == query.EmployeeId &&
                                   x.StoreId == query.StoreId &&
                                   query.TimezoneDate >= x.Survey.StartDate &&
                                  (x.Survey.EndDate.HasValue ? query.TimezoneDate <= x.Survey.EndDate : true) &&
                                   // Exclude surveys that have already been created by the employee for the store
                                   // Recurring surveys: Created once.    
                                   // Non-recurring surveys: Created on the time-zone date. 
                                   !x.Survey.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == query.TimezoneDate) ||
                                                                        e.Survey.IsRecurring == false) &&
                                                                        e.EmployeeId == query.EmployeeId &&
                                                                        e.StoreId == query.StoreId &&
                                                                        e.SurveyId == x.SurveyId));
        }
    }
}
