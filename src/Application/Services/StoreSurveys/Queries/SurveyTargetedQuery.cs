using Engage.Application.Services.StoreSurveys.Models;
using System.Linq.Dynamic.Core;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class SurveyTargetedQuery : IRequest<DataResult<StoreSurveysDto>>
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public DateTime TimezoneDate { get; set; }
    }

    public class SurveyTargetedHandler : BaseQueryHandler, IRequestHandler<SurveyTargetedQuery, DataResult<StoreSurveysDto>>
    {
        private readonly FeatureSwitchOptions _featureSwitch;
        public SurveyTargetedHandler(IAppDbContext context, IMapper mapper, IOptions<FeatureSwitchOptions> featureSwitch) : base(context, mapper)
        {
            _featureSwitch = featureSwitch.Value;
        }

        public async Task<DataResult<StoreSurveysDto>> Handle(SurveyTargetedQuery query, CancellationToken cancellationToken)
        {
            // Survey Filtering
            var currentSurveys = await _context.Surveys.Where(x => x.Disabled == false && x.Deleted == false &&
                                                               query.TimezoneDate >= x.StartDate &&
                                                               (!x.EndDate.HasValue || query.TimezoneDate <= x.EndDate) &&
                                                               !x.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == query.TimezoneDate) || e.Survey.IsRecurring == false) &&
                                                                                             e.EmployeeId == query.EmployeeId && e.StoreId == query.StoreId && e.SurveyId == x.SurveyId))
                                                        .ToListAsync(cancellationToken);

            // Store Filtered
            var currentSurveyIds = currentSurveys.Select(e => e.SurveyId)
                                                 .ToList();

            var surveyStoreIds = currentSurveyIds.Count > 0
                ? await _context.SurveyStores.Where(e => e.StoreId == query.StoreId && currentSurveyIds.Contains(e.SurveyId))
                                             .Select(e => e.SurveyId)
                                             .ToListAsync(cancellationToken)
                : new List<int>();

            // Employee Filtered
            var employeeCurrentSurveyIds = currentSurveys.Where(e => e.IsEmployeeTargeting == true)
                                                         .Select(e => e.SurveyId)
                                                         .ToList();

            var employeeSurveyIds = employeeCurrentSurveyIds.Count > 0
                ? await _context.SurveyEmployees.Where(e => e.EmployeeId == query.EmployeeId && employeeCurrentSurveyIds.Contains(e.SurveyId))
                                                .Select(e => e.SurveyId)
                                                .ToListAsync(cancellationToken)
                : new List<int>();

            // Sub Group Filtered
            var subGroupIds = await _context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId)
                                                           .GroupBy(e => e.EngageSubGroupId)
                                                           .Select(e => e.Key)
                                                           .ToListAsync(cancellationToken);

            var subGroupSurveyIds = currentSurveys.Where(e => e.IsEmployeeTargeting == false && subGroupIds.Contains(e.EngageSubGroupId))
                                                  .Select(e => e.SurveyId)
                                                  .ToList();


            var surveyIds = surveyStoreIds.Intersect((employeeSurveyIds).Union(subGroupSurveyIds)).ToList();

            // Fetch Surveys
            var surveys = surveyIds.Count > 0
                ? await _context.Surveys.Where(e => surveyIds.Contains(e.SurveyId))
                                        .Include(e => e.Supplier)
                                        .Include(e => e.EngageSubGroup)
                                        .Include(e => e.EngageBrand)
                                        .Include(e => e.SurveyQuestions)
                                        .ThenInclude(e => e.SurveyQuestionFalseReasons)
                                        .ThenInclude(e => e.QuestionFalseReason)
                                        .Include(e => e.SurveyQuestions)
                                        .ThenInclude(e => e.SurveyQuestionOptions)
                                        .Include(e => e.SurveyQuestions)
                                        .ThenInclude(e => e.Rules)
                                        .Include(e => e.EngageMasterProduct)
                                        .Include(e => e.SurveyQuestions)
                                        .ThenInclude(e => e.EngageVariantProduct)
                                        .OrderBy(e => e.SupplierId)
                                        .ToListAsync(cancellationToken)
                : new List<Survey>();

            var storeName = await _context.Stores.Where(e => e.StoreId == query.StoreId)
                                                 .Select(e => e.Name)
                                                 .SingleAsync(cancellationToken);

            var surveyResults = surveys.Select(survey => new StoreSurveyResult
            {
                EmployeeId = query.EmployeeId,
                StoreId = query.StoreId,
                StoreName = storeName,
                SupplierId = survey.SupplierId,
                SupplierName = survey.Supplier.Name,
                SurveyId = survey.SurveyId,
                SurveyTitle = survey.Title,
                SurveyNote = survey.Note,
                SurveyDate = DateTime.UtcNow,
                EngageSubGroupId = survey.EngageSubGroupId,
                EngageSubGroupName = survey.EngageSubGroup.Name,
                EngageBrandId = survey.EngageBrandId,
                EngageBrandName = survey.EngageBrand.Name,
                EnableMasterProductOnSurvey = _featureSwitch.EnableMasterProductOnSurvey,
                EngageMasterProductId = survey.EngageMasterProductId.HasValue ? new OptionDto(survey.EngageMasterProductId.Value, survey.EngageMasterProduct.Name) : null,
                Questions = survey.SurveyQuestions.Select(question => _mapper.Map<SurveyQuestion, StoreSurveyQuestionDto>(question))
                                                  .OrderBy(e => e.DisplayOrder)
                                                  .ToList(),
                IsRequired = survey.IsRequired,
                IsDisabled = survey.IsDisabled,
                IsRecurring = survey.IsRecurring,
            }).ToList();

            var store = await _context.Stores.Include(x => x.DCAccounts).Where(x => x.StoreId == query.StoreId).FirstOrDefaultAsync(cancellationToken);

            var dcids = store.DCAccounts.Select(d => d.DistributionCenterId).ToList();

            if (dcids.Count > 0)
            {
                foreach (var survey in surveyResults)
                {
                    foreach (var question in survey.Questions)
                    {
                        if (question.EngageVariantProductId != null)
                        {
                            var productIds = await _context.DCProducts.Include(x => x.DistributionCenter).Where(d => d.EngageVariantProductId == question.EngageVariantProductId.Id && dcids.Contains(d.DistributionCenterId)).ToListAsync(cancellationToken);
                            if (productIds.Count > 0)
                            {
                                question.EngageDcProductIds = productIds.Select(x => new OptionDto() { Id = x.DCProductId, Name = $"{x.Name} - {x.Code} - {x.DistributionCenter.Name}" }).ToList();
                            }
                        }
                    }
                }
            }

            var transformedSurveys = StoreSurveyTransforms.GroupBySupplier(_mapper, surveyResults, storeName);

            if (transformedSurveys != null)
            {
                transformedSurveys.StoreId = query.StoreId;
                transformedSurveys.EmployeeId = query.EmployeeId;
                transformedSurveys.StoreName = storeName;
            }
            else
            {
                transformedSurveys = new StoreSurveysDto()
                {
                    StoreId = query.StoreId,
                    EmployeeId = query.EmployeeId,
                    StoreName = storeName
                };
            }

            return new DataResult<StoreSurveysDto>()
            {
                Data = transformedSurveys
            };
        }
    }
}

