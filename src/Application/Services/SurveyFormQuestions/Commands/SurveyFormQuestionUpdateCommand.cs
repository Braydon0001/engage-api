using Engage.Application.Services.SurveyFormQuestionOptions.Commands;
using Engage.Application.Services.SurveyFormQuestionProducts.Commands;
using Engage.Application.Services.SurveyFormQuestionReasons.Commands;
using System.Data;
using System.Text.RegularExpressions;

namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionUpdateCommand : IMapTo<SurveyFormQuestion>, IRequest<SurveyFormQuestion>
{
    public int Id { get; set; }
    public string QuestionText { get; init; }
    public int? DisplayOrder { get; set; }
    public bool IsRequired { get; init; }
    public string Notes { get; init; }
    public int SurveyFormQuestionGroupId { get; init; }
    public int SurveyFormQuestionTypeId { get; init; }
    public bool IsReasonRequired { get; init; }
    public bool? IsFalseReasonRequired { get; init; }
    public DateTime? MinDateTime { get; init; }
    public DateTime? MaxDateTime { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public List<ReasonOption> AnswerReasons { get; init; }
    public List<ReasonOption> AnswerOptions { get; init; }
    public List<JsonLink> Links { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionUpdateCommand, SurveyFormQuestion>();
    }
}

public record SurveyFormQuestionUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionUpdateCommand, SurveyFormQuestion>
{
    public async Task<SurveyFormQuestion> Handle(SurveyFormQuestionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup).Include(e => e.SurveyFormQuestionType).SingleOrDefaultAsync(e => e.SurveyFormQuestionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        //changing groups
        if (entity.SurveyFormQuestionGroupId != command.SurveyFormQuestionGroupId)
        {
            //check if the question can move without violating rules
            var targetGroup = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId).FirstOrDefaultAsync(cancellationToken);

            //if the question is moving to a group later in the survey
            if (entity.SurveyFormQuestionGroup.DisplayOrder < targetGroup.DisplayOrder)
            {
                //find the first question that references the moving question
                var succeedingQuestions = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup)
                                                                           .Where(e => e.SurveyFormQuestionGroup.SurveyFormId == entity.SurveyFormQuestionGroup.SurveyFormId
                                                                                        && (e.SurveyFormQuestionGroup.DisplayOrder > entity.SurveyFormQuestionGroup.DisplayOrder
                                                                                            || (e.SurveyFormQuestionGroupId == entity.SurveyFormQuestionGroupId
                                                                                                && e.DisplayOrder > entity.DisplayOrder)))
                                                                           .OrderBy(e => e.SurveyFormQuestionGroup.DisplayOrder)
                                                                           .ThenBy(e => e.DisplayOrder)
                                                                           .ToListAsync(cancellationToken);

                //or find the first group that references the question
                var succeedingGroups = succeedingQuestions.Select(e => e.SurveyFormQuestionGroup).Distinct().ToList();

                //throw an error if the question to be disabled is referenced in another question
                if (succeedingQuestions.Count != 0 || succeedingGroups.Count != 0)
                {
                    foreach (var group in succeedingGroups)
                    {
                        var succeedingGroupRules = group.Rules;
                        if (succeedingGroupRules != null)
                        {
                            foreach (var rule in succeedingGroupRules)
                            {
                                var referencedQuestions = GetRuleReferencedQuestions(rule);
                                if (referencedQuestions != null && referencedQuestions.Contains(entity.SurveyFormQuestionId) && group.DisplayOrder <= targetGroup.DisplayOrder)
                                {
                                    throw new Exception("This Question cannot be moved to the selected group (\"" + targetGroup.Name + "\"). It is referenced in another groups' (\"" + group.Name + "\") rules");
                                }
                            }
                        }
                    }

                    foreach (var question in succeedingQuestions)
                    {
                        var succeedingQuestionRules = question.Rules;
                        if (succeedingQuestionRules != null)
                        {
                            foreach (var rule in succeedingQuestionRules)
                            {
                                var referencedQuestions = GetRuleReferencedQuestions(rule);
                                if (referencedQuestions != null && referencedQuestions.Contains(entity.SurveyFormQuestionId) && question.SurveyFormQuestionGroup.DisplayOrder <= targetGroup.DisplayOrder)
                                {
                                    throw new Exception("This Question cannot be moved to the selected group (\"" + targetGroup.Name + "\"). It is referenced in another questions' (\"" + question.QuestionText + "\" in group \"" + question.SurveyFormQuestionGroup.Name + "\") rules");
                                }
                            }
                        }
                    }
                }
            }

            //if the question is moving to a group earlier in the survey
            if (entity.SurveyFormQuestionGroup.DisplayOrder > targetGroup.DisplayOrder)
            {
                //find the first question that references the moving question
                var precedingQuestions = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup)
                                                                           .Where(e => e.SurveyFormQuestionGroup.SurveyFormId == entity.SurveyFormQuestionGroup.SurveyFormId
                                                                                        && (e.SurveyFormQuestionGroup.DisplayOrder < entity.SurveyFormQuestionGroup.DisplayOrder
                                                                                            || (e.SurveyFormQuestionGroupId == entity.SurveyFormQuestionGroupId
                                                                                                && e.DisplayOrder < entity.DisplayOrder)))
                                                                           .OrderBy(e => e.SurveyFormQuestionGroup.DisplayOrder)
                                                                           .ThenBy(e => e.DisplayOrder)
                                                                           .ToListAsync(cancellationToken);

                //get all the questions the moving question references in rules
                var referencedQuestions = new List<int>();
                foreach (var rule in entity.Rules)
                {
                    var refs = GetRuleReferencedQuestions(rule);
                    if (refs.Count != 0)
                    {
                        referencedQuestions.AddRange(refs);
                    }
                }

                //if any of those questions' groups come after the target group, we cannot move
                var referencedQuestionGroups = await Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup)
                                                                                .Where(e => referencedQuestions.Contains(e.SurveyFormQuestionId))
                                                                                .Select(e => e.SurveyFormQuestionGroup.DisplayOrder)
                                                                                .ToListAsync(cancellationToken);

                if (referencedQuestionGroups.Any(e => e > targetGroup.DisplayOrder))
                {
                    throw new Exception("This Question cannot be moved to the selected group (\"" + targetGroup.Name + "\"). It is references questions that would come after it in the survey if it was moved here");
                }
            }

            //remove question from the display order of the old group
            var followingQuestions = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == entity.SurveyFormQuestionGroupId).Where(e => e.DisplayOrder > entity.DisplayOrder).ToListAsync(cancellationToken);
            foreach (var question in followingQuestions)
            {
                question.DisplayOrder--;
            }

            //add the question to the back of the new group
            var newDisplayOrder = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);
            command.DisplayOrder = newDisplayOrder == null ? 1 : newDisplayOrder + 1;
        }
        else command.DisplayOrder ??= entity.DisplayOrder;

        if (command.IsFalseReasonRequired != null)
        {
            entity.Metadata ??= [];
            var currentFlag = entity.Metadata.FirstOrDefault(e => e.Name == "IsFalseReasonRequired");
            if (currentFlag != null)
            {
                var newMeta = new List<JsonSetting>(entity.Metadata);
                newMeta.Remove(currentFlag);
                var newFlag = new JsonSetting()
                {
                    Name = "IsFalseReasonRequired",
                    Type = "Flag",
                    Value = command.IsFalseReasonRequired.Value.ToString(),
                };
                newMeta.Add(newFlag);
                entity.Metadata = newMeta;
            }
            else
            {
                var newMeta = new List<JsonSetting>();
                var newFlag = new JsonSetting()
                {
                    Name = "IsFalseReasonRequired",
                    Type = "Flag",
                    Value = command.IsFalseReasonRequired.Value.ToString(),
                };
                newMeta.Add(newFlag);
                entity.Metadata = newMeta;
            }
        }

        var mappedEntity = Mapper.Map(command, entity);

        if (mappedEntity != null && command.Links != null && command.Links.Count > 0)
        {
            mappedEntity.Links = command.Links;
        }
        else if (command.Links == null || command.Links.Count == 0)
        {
            mappedEntity.Links = null;
        }

        await Mediator.Send(new SurveyFormQuestionProductBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);

        await Mediator.Send(new SurveyFormQuestionReasonBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, AnswerReasons = command.AnswerReasons }, cancellationToken);

        if (entity.SurveyFormQuestionType.Name == "Checkbox" || entity.SurveyFormQuestionType.Name == "Radio")
        {
            await Mediator.Send(new SurveyFormQuestionOptionBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, AnswerOptions = command.AnswerOptions }, cancellationToken);
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }

    public static List<int> GetRuleReferencedQuestions(JsonRule rule)
    {
        var pattern = @",\""name\"":\""q(\d+)(r)?\"",";

        MatchCollection questionNames = rule.Value.Matches(pattern);

        var referencedQuestions = new List<int>();

        foreach (Match questionName in questionNames)
        {
            var questionIdString = questionName.Groups[1].Value;
            if (String.IsNullOrEmpty(questionIdString))
            {
                throw new Exception("Cannot extract Question Id");
            }
            var questionId = Int32.Parse(questionIdString);
            referencedQuestions.Add(questionId);
        }

        return referencedQuestions;
    }
}

public class UpdateSurveyFormQuestionValidator : AbstractValidator<SurveyFormQuestionUpdateCommand>
{
    public UpdateSurveyFormQuestionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.QuestionText).NotEmpty();
        RuleFor(e => e.DisplayOrder);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.Notes);
        RuleFor(e => e.SurveyFormQuestionGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsReasonRequired);
        RuleFor(e => e.MinDateTime);
        RuleFor(e => e.MaxDateTime);
    }
}

public class ReasonOption
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool CompleteSurvey { get; set; }
}