namespace Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormPosHistoryQuery : IRequest<SurveyFormWithHistory>
{
    public int StoreId { get; set; }
}

public record SurveyFormPosHistoryHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IUserService User) : IRequestHandler<SurveyFormPosHistoryQuery, SurveyFormWithHistory>
{
    public async Task<SurveyFormWithHistory> Handle(SurveyFormPosHistoryQuery query, CancellationToken cancellationToken)
    {
        var userId = await Context.Users.Where(e => e.Email == User.UserName)
                               .Select(e => e.UserId)
                               .FirstOrDefaultAsync(cancellationToken);
        var employeeId = 0;
        if (userId != 0)
        {
            employeeId = await Context.Employees.Where(e => e.UserId == userId)
                                                     .Select(e => e.EmployeeId)
                                                     .FirstOrDefaultAsync(cancellationToken);
        }

        //all surveys that have targeting applied
        var targetedSurveyForms = await Context.SurveyFormTargets.Where(e => e.SurveyForm.Disabled == false && e.SurveyForm.IsDisabled == false
                                                                              && DateTime.Now.Date >= e.SurveyForm.StartDate.Value.Date
                                                                              && (!e.SurveyForm.EndDate.HasValue || DateTime.Now.Date <= e.SurveyForm.EndDate.Value.Date)
                                                                              && e.SurveyForm.IsTemplate == false)
                                                                  .Distinct()
                                                                  .ToListAsync(cancellationToken);

        //surrveyIds of surveys that exclude this employee or store
        var excludedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormExcludedStore>()
                                                              .Where(e => e.ExcludedStoreId == query.StoreId)
                                                              .Select(e => e.SurveyFormId)
                                                       .Distinct()
                                                       .ToList();

        var surveyFormIds = new List<int>();

        if (employeeId != 0)
        {
            //This whole query's performance can be improved. Look at SurveyFormSupplierStoreHistoryQuery
            //Consider combining this with SurveyFormPosUpdateSurveyTargetedQuery. There is significant overlap and the calls are made at the same time.
            var employee = await Context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                                   .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                                   .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                                   .Include(e => e.EmployeeDivisions).ThenInclude(e => e.EmployeeDivision)
                                                   .Include(e => e.EmployeeStores)
                                                   .SingleOrDefaultAsync(e => e.EmployeeId == employeeId, cancellationToken);

            if (employee == null)
            {
                return null;
            }

            var employeeSubgroups = employee.EmployeeStores.Select(e => e.EngageSubGroupId).ToList();

            var employeeEngageRegionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false)
                                                                      .Select(e => e.EngageRegionId)
                                                                      .ToList();

            var engageDepartmentIds = employee.EmployeeDepartments.Where(e => e.EngageDepartment.Disabled == false)
                                                                  .Select(e => e.EngageDepartmentId)
                                                                  .ToList();

            var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false
                                                            && e.EmployeeJobTitle.Level == 3)
                                                        .Select(e => e.EmployeeJobTitleId)
                                                        .ToList();

            var employeeDivisionIds = employee.EmployeeDivisions.Where(e => e.EmployeeDivision.Disabled == false)
                                                                .Select(e => e.EmployeeDivisionId)
                                                                .ToList();

            var targetedSurveyFormIds = targetedSurveyForms.Select(e => e.SurveyFormId).Distinct().ToList();

            var employeeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployee>().Select(e => e.SurveyFormId).ToList();
            var employeeEngageRegionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.SurveyFormId).ToList();
            var engageDepartmentTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEngageDepartment>().Select(e => e.SurveyFormId).ToList();
            var employeeJobTitleTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.SurveyFormId).ToList();
            var employeeDivisionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormEmployeeDivision>().Select(e => e.SurveyFormId).ToList();

            //employee targeted surveys
            var employeeTargetingSurveyFormIds = employeeTargetedSurveyFormIds.Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                              .Union(employeeEngageRegionTargetedSurveyFormIds)
                                                                              .Union(engageDepartmentTargetedSurveyFormIds)
                                                                              .Union(employeeJobTitleTargetedSurveyFormIds)
                                                                              .Union(employeeDivisionTargetedSurveyFormIds)
                                                                              .Distinct()
                                                                              .ToList();

            var storeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStore>().Select(e => e.SurveyFormId).ToList();
            var storeEngageRegionTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreEngageRegion>().Select(e => e.SurveyFormId).ToList();
            var storeClusterTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreCluster>().Select(e => e.SurveyFormId).ToList();
            var storeFormatTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreFormat>().Select(e => e.SurveyFormId).ToList();
            var storeLSMTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreLSM>().Select(e => e.SurveyFormId).ToList();
            var storeTypeTargetedSurveyFormIds = targetedSurveyForms.OfType<SurveyFormStoreType>().Select(e => e.SurveyFormId).ToList();

            //store targeted surveys
            var storeTargetingSurveyFormIds = storeTargetedSurveyFormIds.Union(storeEngageRegionTargetedSurveyFormIds)
                                                                        .Union(storeClusterTargetedSurveyFormIds)
                                                                        .Union(storeFormatTargetedSurveyFormIds)
                                                                        .Union(storeLSMTargetedSurveyFormIds)
                                                                        .Union(storeTypeTargetedSurveyFormIds)
                                                                        .Distinct()
                                                                        .ToList();

            //surveys that have employee and store targeting
            var combinedTargetingSurveyFormIds = employeeTargetingSurveyFormIds.Intersect(storeTargetingSurveyFormIds).Distinct().ToList();

            //employee-only targeted surveys
            var employeeOnlyTargetedSurveyFormIds = employeeTargetingSurveyFormIds.Where(e => !combinedTargetingSurveyFormIds.Contains(e)).Distinct().ToList();

            //store-only targeted surveys
            var storeOnlyTargetedSurveyFormIds = storeTargetingSurveyFormIds.Where(e => !combinedTargetingSurveyFormIds.Contains(e)).Distinct().ToList();

            foreach (var surveyFormId in employeeOnlyTargetedSurveyFormIds)
            {
                var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

                var isEmployeeTargeted = false;

                //if we are targeting by employee
                if (surveyTargetings.OfType<SurveyFormEmployee>().ToList().Count > 0)
                {
                    isEmployeeTargeted = surveyTargetings.OfType<SurveyFormEmployee>()
                                                 .Where(e => e.EmployeeId == employee.EmployeeId)
                                                 .Distinct()
                                                 .Any();
                }

                var isCriteriaTargeted = true;
                var employeeCriteriaSkipped = true;

                if (surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>().ToList().Count > 0)
                {
                    isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>()
                                                               .Where(e => employeeEngageRegionIds.Contains(e.EmployeeEngageRegionId) && e.EmployeeEngageRegion.Disabled == false)
                                                               .Distinct()
                                                               .Any();
                    employeeCriteriaSkipped = false;
                }

                if (surveyTargetings.OfType<SurveyFormEngageDepartment>().ToList().Count > 0)
                {
                    isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEngageDepartment>()
                                                               .Where(e => engageDepartmentIds.Contains(e.EngageDepartmentId)
                                                                                                        && e.EngageDepartment.Disabled == false)
                                                               .Distinct()
                                                               .Any();
                    employeeCriteriaSkipped = false;
                }

                if (surveyTargetings.OfType<SurveyFormEmployeeJobTitle>().ToList().Count > 0)
                {
                    isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeJobTitle>()
                                                               .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId)
                                                                                              && e.EmployeeJobTitle.Disabled == false
                                                                                              && e.EmployeeJobTitle.Level == 3)
                                                               .Distinct()
                                                               .Any();
                    employeeCriteriaSkipped = false;
                }

                if (surveyTargetings.OfType<SurveyFormEmployeeDivision>().ToList().Count > 0)
                {
                    isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeDivision>()
                                                               .Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)
                                                                                                  && e.EmployeeDivision.Disabled == false)
                                                               .Distinct()
                                                               .Any();
                    employeeCriteriaSkipped = false;
                }

                if (employeeCriteriaSkipped)
                {
                    isCriteriaTargeted = false;
                }

                if (isEmployeeTargeted || isCriteriaTargeted)
                {
                    surveyFormIds.Add(surveyFormId);
                }
            }

            var store = await Context.Stores.Include(e => e.EngageRegion)
                                             .Include(e => e.StoreCluster)
                                             .Include(e => e.StoreFormat)
                                             .Include(e => e.StoreLSM)
                                             .Include(e => e.StoreType)
                                             .SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);

            if (store != null)
            {

                foreach (var surveyFormId in storeOnlyTargetedSurveyFormIds)
                {
                    var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

                    var isStoreTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormStore>().ToList().Count > 0)
                    {
                        isStoreTargeted = surveyTargetings.OfType<SurveyFormStore>()
                                                     .Where(e => e.StoreId == store.StoreId)
                                                     .Distinct()
                                                     .Any();
                    }

                    var isCriteriaTargeted = true;
                    var storeCriteriaSkipped = true;

                    if (surveyTargetings.OfType<SurveyFormStoreEngageRegion>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreEngageRegion>()
                                                                   .Where(e => e.StoreEngageRegionId == store.EngageRegionId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreCluster>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreCluster>()
                                                                   .Where(e => e.StoreClusterId == store.StoreClusterId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreFormat>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreFormat>()
                                                                   .Where(e => e.StoreFormatId == store.StoreFormatId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreLSM>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreLSM>()
                                                                   .Where(e => e.StoreLSMId == store.StoreLSMId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreType>().ToList().Count > 0)
                    {
                        isCriteriaTargeted = isCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreType>()
                                                                   .Where(e => e.StoreTypeId == store.StoreTypeId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (storeCriteriaSkipped)
                    {
                        isCriteriaTargeted = false;
                    }

                    if (isStoreTargeted || isCriteriaTargeted)
                    {
                        surveyFormIds.Add(surveyFormId);
                    }
                }

                foreach (var surveyFormId in combinedTargetingSurveyFormIds)
                {
                    var surveyTargetings = targetedSurveyForms.Where(e => e.SurveyFormId == surveyFormId).ToList();

                    var isEmployeeTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormEmployee>().ToList().Count > 0)
                    {
                        isEmployeeTargeted = surveyTargetings.OfType<SurveyFormEmployee>()
                                                     .Where(e => e.EmployeeId == employee.EmployeeId)
                                                     .Distinct()
                                                     .Any();
                    }

                    var isEmployeeCriteriaTargeted = true;
                    var employeeCriteriaSkipped = true;


                    if (surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeEngageRegion>()
                                                                   .Where(e => employeeEngageRegionIds.Contains(e.EmployeeEngageRegionId) && e.EmployeeEngageRegion.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEngageDepartment>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEngageDepartment>()
                                                                   .Where(e => engageDepartmentIds.Contains(e.EngageDepartmentId)
                                                                                                            && e.EngageDepartment.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEmployeeJobTitle>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeJobTitle>()
                                                                   .Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId)
                                                                                                  && e.EmployeeJobTitle.Disabled == false
                                                                                                  && e.EmployeeJobTitle.Level == 3)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormEmployeeDivision>().ToList().Count > 0)
                    {
                        isEmployeeCriteriaTargeted = isEmployeeCriteriaTargeted && surveyTargetings.OfType<SurveyFormEmployeeDivision>()
                                                                   .Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)
                                                                                                      && e.EmployeeDivision.Disabled == false)
                                                                   .Distinct()
                                                                   .Any();
                        employeeCriteriaSkipped = false;
                    }

                    if (employeeCriteriaSkipped)
                    {
                        isEmployeeCriteriaTargeted = false;
                    }

                    var isStoreTargeted = false;

                    if (surveyTargetings.OfType<SurveyFormStore>().ToList().Count > 0)
                    {
                        isStoreTargeted = surveyTargetings.OfType<SurveyFormStore>()
                                                     .Where(e => e.StoreId == store.StoreId)
                                                     .Distinct()
                                                     .Any();
                        employeeCriteriaSkipped = false;

                    }

                    if (employeeCriteriaSkipped)
                    {
                        isEmployeeCriteriaTargeted = false;
                    }

                    var isStoreCriteriaTargeted = true;
                    var storeCriteriaSkipped = true;

                    if (surveyTargetings.OfType<SurveyFormStoreEngageRegion>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreEngageRegion>()
                                                                   .Where(e => e.StoreEngageRegionId == store.EngageRegionId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreCluster>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreCluster>()
                                                                   .Where(e => e.StoreClusterId == store.StoreClusterId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreFormat>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreFormat>()
                                                                   .Where(e => e.StoreFormatId == store.StoreFormatId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreLSM>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreLSM>()
                                                                   .Where(e => e.StoreLSMId == store.StoreLSMId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (surveyTargetings.OfType<SurveyFormStoreType>().ToList().Count > 0)
                    {
                        isStoreCriteriaTargeted = isStoreCriteriaTargeted && surveyTargetings.OfType<SurveyFormStoreType>()
                                                                   .Where(e => e.StoreTypeId == store.StoreTypeId)
                                                                   .Distinct()
                                                                   .Any();
                        storeCriteriaSkipped = false;
                    }

                    if (storeCriteriaSkipped)
                    {
                        isStoreCriteriaTargeted = false;
                    }

                    if ((isEmployeeTargeted || isEmployeeCriteriaTargeted) && (isStoreTargeted || isStoreCriteriaTargeted))
                    {
                        surveyFormIds.Add(surveyFormId);
                    }
                }
            }
        }

        var entities = await Context.SurveyFormSubmissions
                                                  .Include(e => e.SurveyFormAnswers)
                                                  .Include(e => e.SurveyForm)
                                                  .Where(e => e.StoreId == query.StoreId && e.SurveyForm.SurveyFormType.Name == "POS Update" && (surveyFormIds.Count > 0 && surveyFormIds.Contains(e.SurveyFormId)))
                                                  .OrderByDescending(e => e.Created)
                                                  //.Take(10)
                                                  .ProjectTo<SurveyFormHistoryDto>(Mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);
        //var list = new List<SurveyFormHistoryDto>(entities);
        var surveyIds = entities.Select(e => e.SurveyFormId).Distinct().ToList();

        if (entities.Count > 0)
        {
            foreach (var entity in entities)
            {
                var answers = await Context.SurveyFormAnswers.Where(e => e.SurveyFormSubmissionId == entity.SurveyFormSubmissionId)
                                                                   .ProjectTo<SurveyFormAnswserHistoryDto>(Mapper.ConfigurationProvider)
                                                                   .ToListAsync(cancellationToken);
                entity.Answers = answers;
            }
        }

        // This will need to be changed if they need more POS update surveys
        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking()
                                           .Where(e => !e.IsDisabled && surveyIds.Contains(e.SurveyFormId))
                                           .Where(e => !excludedSurveyFormIds.Contains(e.SurveyFormId));

        var surveys = await queryable.Include(e => e.SurveyFormProducts)
                                     .Include(e => e.SurveyFormType)
                                     .Include(e => e.Supplier)
                                     .Include(e => e.EngageSubGroup)
                                     .Include(e => e.EngageBrand)
                                     .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                         .ThenInclude(e => e.SurveyFormQuestionGroupProducts)
                                     .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                         .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                             .ThenInclude(e => e.SurveyFormQuestionProducts)
                                                 .ThenInclude(e => e.EngageVariantProduct)
                                     .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                         .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                             .ThenInclude(e => e.SurveyFormQuestionOptions)
                                                 .ThenInclude(e => e.SurveyFormOption)
                                     .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                         .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                             .ThenInclude(e => e.SurveyFormQuestionReasons)
                                                 .ThenInclude(e => e.SurveyFormReason)
                                     .Include(e => e.SurveyFormQuestionGroups.OrderBy(e => e.DisplayOrder).Where(e => e.SurveyFormQuestions.Any()))
                                         .ThenInclude(e => e.SurveyFormQuestions.OrderBy(e => e.DisplayOrder))
                                             .ThenInclude(e => e.SurveyFormQuestionType)
                                     .ToListAsync(cancellationToken);



        var surveyForms = Mapper.Map<List<SurveyFormWithQuestions>>(surveys);

        var SurveyFormWithHistory = new SurveyFormWithHistory { History = entities, SurveyForms = surveyForms };

        return SurveyFormWithHistory;
    }

}

public class SurveyFormWithHistory
{
    public List<SurveyFormWithQuestions> SurveyForms { get; set; }
    public List<SurveyFormHistoryDto> History { get; set; }
}

