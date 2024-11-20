
namespace Engage.Application.Services.StoreConcepts.Queries;

public class SurveyFormConceptGetByTargetQuery : IRequest<StoreConceptSurveyFormAdvancedTargetingVm>
{
    public int Id { get; set; }
}
public record SurveyFormConceptGetByTargetHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormConceptGetByTargetQuery, StoreConceptSurveyFormAdvancedTargetingVm>
{
    public async Task<StoreConceptSurveyFormAdvancedTargetingVm> Handle(SurveyFormConceptGetByTargetQuery query, CancellationToken cancellationToken)
    {
        var store = await Context.Stores
                                 .Include(e => e.StoreFormat)
                                 .SingleOrDefaultAsync(e => e.StoreId == query.Id, cancellationToken);

        if (store.IsNull())
        {
            return null;
        }

        var surveyIds = await Context.SurveyForms
                                   .AsNoTracking()
                                   .Where(e => e.SurveyFormTypeId == (int)SurveyFormTypeId.Concept
                                        && e.StartDate.Value.Date <= DateTime.Now
                                        && (e.EndDate == null || e.EndDate.Value.Date >= DateTime.Now.Date)
                                        && e.IsDisabled == false)
                                   .Select(e => e.SurveyFormId)
                                   .ToListAsync(cancellationToken);

        var storeSurveyForms = await Context.SurveyFormStores
                                            .AsNoTracking()
                                            .Where(e => surveyIds.Contains(e.SurveyFormId)
                                                && e.StoreId == query.Id)
                                            .ToListAsync(cancellationToken);

        var storeEngageRegionForms = await Context.SurveyFormStoreEngageRegions
                                                  .AsNoTracking()
                                                  .Where(e => surveyIds.Contains(e.SurveyFormId)
                                                    && e.StoreEngageRegionId == store.EngageRegionId)
                                                  .ToListAsync(cancellationToken);

        var storeFormatForms = await Context.SurveyFormStoreFormats
                                            .AsNoTracking()
                                            .Where(e => surveyIds.Contains(e.SurveyFormId)
                                                && e.StoreFormatId == store.StoreFormatId)
                                            .ToListAsync(cancellationToken);

        List<int> targetedSurveyIds = storeSurveyForms.Select(e => e.SurveyFormId)
                                                      .Union(storeEngageRegionForms.Select(e => e.SurveyFormId))
                                                      .Union(storeFormatForms.Select(e => e.SurveyFormId))
                                                      .Distinct()
                                                      .ToList();

        var concepts = await Context.SurveyForms.AsNoTracking()
                                               .Where(e => targetedSurveyIds.Contains(e.SurveyFormId))
                                               .Include(e => e.SurveyFormQuestionGroups)
                                               .ThenInclude(e => e.SurveyFormQuestions)
                                               .ProjectTo<SurveyFormConceptDto>(Mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        var firstQuestionIds = concepts.Select(e => e.FirstQuestionId).ToList();

        var submissions = await Context.SurveyFormSubmissions.AsNoTracking()
                                                                .Where(e => e.StoreId == query.Id
                                                                    && targetedSurveyIds.Contains(e.SurveyFormId)
                                                                    && e.IsComplete == true)
                                                                .OrderByDescending(e => e.CompletedDate)
                                                                .ToListAsync(cancellationToken);

        var questionAnswers = await Context.SurveyFormAnswers.AsNoTracking().Where(e => firstQuestionIds.Contains(e.SurveyFormQuestionId)
                                                                && submissions.Select(e => e.SurveyFormSubmissionId).ToList().Contains(e.SurveyFormSubmissionId))
                                                             .Include(e => e.SurveyFormQuestion)
                                                             .ThenInclude(e => e.SurveyFormQuestionType)
                                                             .Include(e => e.SurveyFormAnswerOptions)
                                                             .ThenInclude(e => e.SurveyFormOption)
                                                             .ToListAsync(cancellationToken);

        foreach (var concept in concepts)
        {
            var conceptSubmissions = submissions.Where(e => e.SurveyFormId == concept.Id).OrderByDescending(e => e.CompletedDate).ToList();
            if (conceptSubmissions == null || conceptSubmissions.Count == 0) continue;

            var submission = conceptSubmissions.First();
            var submissionFirstAnswer = questionAnswers.FirstOrDefault(e => e.SurveyFormSubmissionId == submission.SurveyFormSubmissionId);
            if (submission != null)
            {
                concept.CompletedDate = submission.CompletedDate;
                concept.SurveyFormSubmissionId = submission.SurveyFormSubmissionId;
            }
            if (submissionFirstAnswer != null)
            {
                concept.FirstQuestionAnswer = submissionFirstAnswer.AnswerText;
                concept.FirstQuestionType = submissionFirstAnswer.SurveyFormQuestion.SurveyFormQuestionType.Name;
                var selectedOptions = submissionFirstAnswer.SurveyFormAnswerOptions.Select(e => e.SurveyFormOption.Name).ToList();
                concept.FirstQuestionOptions = string.Join(",", selectedOptions);
            }
            var firstSubmission = conceptSubmissions.Last();
            if (firstSubmission != null)
            {
                concept.FirstCompletionDate = firstSubmission.Created;
            }
        }

        StoreConceptSurveyFormAdvancedTargetingVm returnObject = new();
        returnObject.Stores = [store.StoreId];
        returnObject.StoreEngageRegions = [store.EngageRegionId];
        returnObject.StoreFormats = [store.StoreFormatId];
        returnObject.StoreClusters = [store.StoreClusterId];
        returnObject.StoreLSMs = [store.StoreLSMId];
        returnObject.StoreTypes = [store.StoreTypeId];
        returnObject.SurveyForms = new ListResult<SurveyFormConceptDto>(concepts);
        returnObject.HasAdvancedTargeting = false;

        if (concepts.Count > 0)
        {
            foreach (var concept in concepts)
            {
                if (concept.Rules != null && concept.Rules.Any(e => e.Type == "TargetRule"))
                {
                    returnObject.HasAdvancedTargeting = true;
                    break;
                }
            }
        }


        return returnObject;
    }
}

public class SurveyFormConceptGetByTargetValidator : AbstractValidator<SurveyFormConceptGetByTargetQuery>
{
    public SurveyFormConceptGetByTargetValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}