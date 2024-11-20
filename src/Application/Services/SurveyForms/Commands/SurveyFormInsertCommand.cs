using Engage.Application.Services.SurveyFormProducts.Commands;
using Engage.Application.Services.SurveyFormQuestionGroups.Commands;
using Engage.Application.Services.SurveyFormQuestions.Commands;

namespace Engage.Application.Services.SurveyForms.Commands;

public class SurveyFormInsertCommand : IMapTo<SurveyForm>, IRequest<SurveyForm>
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsRequired { get; init; }
    public bool IsRecurring { get; init; }
    public bool IsDisabled { get; init; }
    public int SurveyFormTypeId { get; init; }
    public int? EngageSubgroupId { get; init; }
    public int? SupplierId { get; init; }
    public int? EngageBrandId { get; init; }
    public bool IsStoreRecurring { get; init; }
    public bool IsEmployeeSurvey { get; init; }
    public bool IgnoreSubgroup { get; init; }
    public List<int> EngageMasterProductIds { get; init; }
    public bool IsTemplate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormInsertCommand, SurveyForm>();
    }
}

public record SurveyFormInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormInsertCommand, SurveyForm>
{
    public async Task<SurveyForm> Handle(SurveyFormInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormInsertCommand, SurveyForm>(command);

        Context.SurveyForms.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        var surveyType = await Context.SurveyFormTypes.Where(e => e.SurveyFormTypeId == command.SurveyFormTypeId).FirstOrDefaultAsync(cancellationToken);

        SurveyFormQuestionGroup defaultGroup = null;

        if (surveyType.Name != "Shelf Spacing")
        {
            //create a default group for questions
            defaultGroup = await Mediator.Send(new SurveyFormQuestionGroupInsertCommand() { SurveyFormId = entity.SurveyFormId, IsRequired = false, Name = "Default" }, cancellationToken);
        }

        //Add the products to the survey
        await Mediator.Send(new SurveyFormProductBatchUpdateCommand() { SurveyFormId = entity.SurveyFormId, EngageMasterProductIds = command.EngageMasterProductIds }, cancellationToken);


        if (surveyType.UseTemplate == true)
        {
            var surveyTemplate = await Context.SurveyForms.Where(e => e.SurveyFormId == surveyType.SurveyFormTemplateId && surveyType.UseTemplate == true)
                                                           .FirstOrDefaultAsync(cancellationToken);
            var surveyTemplateQuestionGroups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions)
                                                                                     .ThenInclude(e => e.SurveyFormQuestionReasons)
                                                                                     .ThenInclude(e => e.SurveyFormReason)
                                                                                     .Where(e => e.SurveyFormId == surveyTemplate.SurveyFormId)
                                                                                     .ToListAsync(cancellationToken);


            if (surveyTemplateQuestionGroups.Count > 0)
            {
                if (defaultGroup.IsNotNull())
                {
                    foreach (var questionGroup in surveyTemplateQuestionGroups)
                    {
                        if (questionGroup.SurveyFormQuestions.Count > 0)
                        {
                            foreach (var question in questionGroup.SurveyFormQuestions)
                            {
                                // create default questions
                                await Mediator.Send(new SurveyFormQuestionInsertCommand()
                                {
                                    QuestionText = question.QuestionText,
                                    DisplayOrder = question.DisplayOrder,
                                    SurveyFormQuestionGroupId = defaultGroup.SurveyFormQuestionGroupId,
                                    SurveyFormQuestionTypeId = question.SurveyFormQuestionTypeId,
                                    AnswerReasons = question.SurveyFormQuestionReasons.Select(e => new ReasonOption() { Text = e.SurveyFormReason.Name }).ToList(),

                                }, cancellationToken);
                            }

                        }
                    }
                }
            }


        }

        return entity;
    }
}

public class SurveyFormInsertValidator : AbstractValidator<SurveyFormInsertCommand>
{
    public SurveyFormInsertValidator()
    {
        RuleFor(e => e.Title).NotEmpty();
        RuleFor(e => e.Description);
        RuleFor(e => e.Note);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.IsRecurring);
        RuleFor(e => e.IsDisabled);
        RuleFor(e => e.SurveyFormTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageSubgroupId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.EngageBrandId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.IsStoreRecurring);
        RuleFor(e => e.IsEmployeeSurvey);
    }
}