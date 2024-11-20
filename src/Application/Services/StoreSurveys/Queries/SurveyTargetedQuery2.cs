using Engage.Application.Services.StoreSurveys.Models;
using System.Linq.Dynamic.Core;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public record SurveyTargetedQuery2(int EmployeeId, int StoreId, DateTime TimezoneDate) : IRequest<DataResult<StoreSurveysDto>>
    {
    }

    public class SurveyTargetedHandler2 : IRequestHandler<SurveyTargetedQuery2, DataResult<StoreSurveysDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly FeatureSwitchOptions _featureSwitch;

        public SurveyTargetedHandler2(IAppDbContext context, IMapper mapper, IOptions<FeatureSwitchOptions> featureSwitch)
        {
            _context = context;
            _mapper = mapper;
            _featureSwitch = featureSwitch.Value;
        }

        public async Task<DataResult<StoreSurveysDto>> Handle(SurveyTargetedQuery2 query, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                                   .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                                   .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);
            if (employee == null)
            {
                return null;
            }

            var store = await _context.Stores.Include(e => e.StoreFormat)
                                             .SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);
            if (store == null)
            {
                return null;
            }


            var storeSurveyTargetIds = await _context.SurveyStoreTargets.Where(e => e.StoreId == query.StoreId && query.TimezoneDate >= e.Survey.StartDate && (!e.Survey.EndDate.HasValue || query.TimezoneDate <= e.Survey.EndDate))
                                                                   .Select(e => e.SurveyId)
                                                                   .ToListAsync(cancellationToken);

            var storeFormatSurveyIds = await _context.SurveyStoreFormatTargets.Where(e => e.StoreFormatId == store.StoreFormatId && store.StoreFormat.Disabled == false)
                                                                               .Select(e => e.SurveyId)
                                                                               .ToListAsync(cancellationToken);

            var employeeSurveyTargetIds = await _context.SurveyEmployeeTargets.Where(e => e.EmployeeId == query.EmployeeId && query.TimezoneDate >= e.Survey.StartDate && (!e.Survey.EndDate.HasValue || query.TimezoneDate <= e.Survey.EndDate))
                                                                         .Select(e => e.SurveyId)
                                                                         .ToListAsync(cancellationToken);

            var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3).Select(e => e.EmployeeJobTitleId).ToList();
            var JobTitleSurveyIds = await _context.SurveyEmployeeJobTitleTargets.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId) && e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3)
                                                                                 .Select(e => e.SurveyId)
                                                                                 .ToListAsync(cancellationToken);

            var regionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();
            var regionSurveyIds = await _context.SurveyEngageRegions.Where(e => regionIds.Contains(e.EngageRegionId) && e.EngageRegion.Disabled == false)
                                                                     .Select(e => e.SurveyId)
                                                                     .ToListAsync(cancellationToken);


            var storeSurveyIds = storeSurveyTargetIds.Union(storeFormatSurveyIds).Distinct().ToList();
            var employeeSurveyIds = employeeSurveyTargetIds.Union(JobTitleSurveyIds).Union(regionSurveyIds).Distinct().ToList();

            var subGroupIds = await _context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId && e.EngageSubGroup.Disabled == false)
                                                           .GroupBy(e => e.EngageSubGroupId)
                                                           .Select(e => e.Key)
                                                           .ToListAsync(cancellationToken);


            var surveys = await _context.Surveys.Where(e => e.Disabled == false && query.TimezoneDate >= e.StartDate && (!e.EndDate.HasValue || query.TimezoneDate <= e.EndDate) &&
                                                            !e.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == query.TimezoneDate) || e.Survey.IsRecurring == false) &&
                                                                                          e.EmployeeId == query.EmployeeId && e.StoreId == query.StoreId && e.SurveyId == e.SurveyId) &&
                                                            storeSurveyIds.Contains(e.SurveyId) &&
                                                            (e.IsEmployeeTargeting == true && employeeSurveyIds.Contains(e.SurveyId)) || (e.IsEmployeeTargeting == false && subGroupIds.Contains(e.SurveyId)))
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
                                                .ToListAsync(cancellationToken);

            var surveyResults = surveys.Select(survey => new StoreSurveyResult
            {
                EmployeeId = query.EmployeeId,
                StoreId = query.StoreId,
                StoreName = store.Name,
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

            var transformedSurveys = StoreSurveyTransforms.GroupBySupplier(_mapper, surveyResults, store.Name);

            if (transformedSurveys != null)
            {
                transformedSurveys.StoreId = query.StoreId;
                transformedSurveys.EmployeeId = query.EmployeeId;
                transformedSurveys.StoreName = store.Name;
            }
            else
            {
                transformedSurveys = new StoreSurveysDto()
                {
                    StoreId = query.StoreId,
                    EmployeeId = query.EmployeeId,
                    StoreName = store.Name
                };
            }

            return new DataResult<StoreSurveysDto>()
            {
                Data = transformedSurveys
            };
        }
    }
}

