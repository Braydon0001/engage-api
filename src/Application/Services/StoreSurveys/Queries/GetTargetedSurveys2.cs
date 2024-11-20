using Engage.Application.Services.StoreSurveys.Models;
using System.Linq.Dynamic.Core;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class GetTargetedSurveys2 : GetQuery, IRequest<DataResult<StoreSurveysDto>>
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public DateTime TimezoneDate { get; set; }
    }

    public class GetTargetedSurveys2QueryHandler : BaseQueryHandler, IRequestHandler<GetTargetedSurveys2, DataResult<StoreSurveysDto>>
    {
        private readonly FeatureSwitchOptions _featureSwitch;
        public GetTargetedSurveys2QueryHandler(IAppDbContext context, IMapper mapper, IOptions<FeatureSwitchOptions> featureSwitch) : base(context, mapper)
        {
            _featureSwitch = featureSwitch.Value;
        }

        public async Task<DataResult<StoreSurveysDto>> Handle(GetTargetedSurveys2 query, CancellationToken cancellationToken)
        {
            // Get surveys for the date range and store 
            var surveysQuery = _context.Surveys.Where(x => query.TimezoneDate >= x.StartDate &&
                                                          x.Disabled != true && x.Deleted != true &&
                                                          (!x.EndDate.HasValue || query.TimezoneDate <= x.EndDate) &&
                                                           !x.SurveyInstances.Any(e => ((e.Survey.IsRecurring == true && e.SurveyDate == query.TimezoneDate) ||
                                                                                         e.Survey.IsRecurring == false) &&
                                                                                         e.EmployeeId == query.EmployeeId &&
                                                                                         e.StoreId == query.StoreId &&
                                                                                         e.SurveyId == x.SurveyId))
                                               .Join(_context.SurveyStores.Where(e => e.StoreId == query.StoreId),
                                                     survey => survey.SurveyId,
                                                     surveyStores => surveyStores.SurveyId,
                                                     (survey, SurveyStores) => survey);

            // Get employee targeted surveys.            
            var employeeSurveyIds = await surveysQuery.Join(_context.SurveyEmployees.Where(e => e.EmployeeId == query.EmployeeId),
                                                            survey => survey.SurveyId,
                                                            surveyEmployee => surveyEmployee.SurveyId,
                                                            (survey, SurveyEmployee) => survey)
                                                      .Select(e => e.SurveyId)
                                                      .ToListAsync(cancellationToken);


            // Get sub group targeted surveys.                         
            // Exclude surveys where only employee targeting must be used.  
            var subGroupSurveyIds = await surveysQuery.Where(e => !e.IsEmployeeTargeting)
                                                      .Join(_context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId),
                                                            survey => survey.EngageSubGroupId,
                                                            employeeStore => employeeStore.EngageSubGroupId,
                                                            (survey, SurveyEmployee) => survey)
                                                      .Select(e => e.SurveyId)
                                                      .ToListAsync(cancellationToken);

            var surveyIds = employeeSurveyIds.Union(subGroupSurveyIds)
                                             .ToList();

            var surveys = await _context.Surveys.Where(e => surveyIds.Contains(e.SurveyId))
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



            foreach (var survey in surveyResults)
            {
                foreach (var question in survey.Questions)
                {
                    question.EngageDcProductIds = new List<OptionDto>();
                    if (question.EngageVariantProductId != null && dcids.Count > 0)
                    {
                        var productIds = await _context.DCProducts.Include(x => x.DistributionCenter).Where(d => d.EngageVariantProductId == question.EngageVariantProductId.Id && dcids.Contains(d.DistributionCenterId)).ToListAsync(cancellationToken);
                        if (productIds.Count > 0)
                        {
                            question.EngageDcProductIds = productIds.Select(x => new OptionDto() { Id = x.DCProductId, Name = $"{x.Name} - {x.Code} - {x.DistributionCenter.Name}" }).ToList();
                        }
                    }
                }
            }


            var transformedSurveys = StoreSurveyTransforms.GroupBySupplier(_mapper, surveyResults, storeName);

            /** 
             * Adding the Store and Employee id to the response.
             * The Mobile app was using the Store Id to submit surveys.
             * It has been updated in mobile and should be release the 
             * next version, then this code would not be required anymore.
             * (Current Version: 0.3.0)
             */

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

