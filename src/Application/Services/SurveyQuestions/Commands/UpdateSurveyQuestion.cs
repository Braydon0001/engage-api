using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Services.SurveyQuestionRules.Commands;
using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class UpdateSurveyQuestionCommand : SurveyQuestionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? Option1Id { get; set; }
    public int? Option2Id { get; set; }
    public int? Option3Id { get; set; }
    public int? Option4Id { get; set; }
    public int? Option5Id { get; set; }
    public int? Option6Id { get; set; }
    public int? Option7Id { get; set; }
    public int? Option8Id { get; set; }
    public int? Option9Id { get; set; }
    public int? Option10Id { get; set; }
}

public class UpdateSurveyQuestionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyQuestionCommand, OperationStatus>
{
    public UpdateSurveyQuestionCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }
    public async Task<OperationStatus> Handle(UpdateSurveyQuestionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyQuestions
            .FirstOrDefaultAsync(x => x.SurveyQuestionId == command.Id);

        if (entity == null)
            throw new NotFoundException(nameof(SurveyQuestion), command.Id);

        _mapper.Map(command, entity);

        if (command.QuestionFalseReasonIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(
                AssignDesc.FALSEREASON_SURVEYQUESTION, entity.SurveyQuestionId, command.QuestionFalseReasonIds));
        }

        var options = new Dictionary<int?, OptionValue>();
        var newOptions = new List<OptionValue>();

        if (!string.IsNullOrWhiteSpace(command.Option1))
        {
            var option1 = new OptionValue { Value = command.Option1, CompleteSurvey = command.CompleteSurvey1 };
            if (command.Option1Id.HasValue)
            {
                options.Add(command.Option1Id.Value, option1);
            }
            else { newOptions.Add(option1); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option2))
        {
            var option2 = new OptionValue { Value = command.Option2, CompleteSurvey = command.CompleteSurvey2 };
            if (command.Option2Id.HasValue)
            {
                options.Add(command.Option2Id.Value, option2);
            }
            else { newOptions.Add(option2); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option3))
        {
            var option3 = new OptionValue { Value = command.Option3, CompleteSurvey = command.CompleteSurvey3 };
            if (command.Option3Id.HasValue)
            {
                options.Add(command.Option3Id.Value, option3);
            }
            else { newOptions.Add(option3); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option4))
        {
            var option4 = new OptionValue { Value = command.Option4, CompleteSurvey = command.CompleteSurvey4 };
            if (command.Option4Id.HasValue)
            {
                options.Add(command.Option4Id.Value, option4);
            }
            else { newOptions.Add(option4); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option5))
        {
            var option5 = new OptionValue { Value = command.Option5, CompleteSurvey = command.CompleteSurvey5 };
            if (command.Option5Id.HasValue)
            {
                options.Add(command.Option5Id.Value, option5);
            }
            else { newOptions.Add(option5); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option6))
        {
            var option6 = new OptionValue { Value = command.Option6, CompleteSurvey = command.CompleteSurvey6 };
            if (command.Option6Id.HasValue)
            {
                options.Add(command.Option6Id.Value, option6);
            }
            else { newOptions.Add(option6); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option7))
        {
            var option7 = new OptionValue { Value = command.Option7, CompleteSurvey = command.CompleteSurvey7 };
            if (command.Option7Id.HasValue)
            {
                options.Add(command.Option7Id.Value, option7);
            }
            else { newOptions.Add(option7); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option8))
        {
            var option8 = new OptionValue { Value = command.Option8, CompleteSurvey = command.CompleteSurvey8 };
            if (command.Option8Id.HasValue)
            {
                options.Add(command.Option8Id.Value, option8);
            }
            else { newOptions.Add(option8); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option9))
        {
            var option9 = new OptionValue { Value = command.Option9, CompleteSurvey = command.CompleteSurvey9 };
            if (command.Option9Id.HasValue)
            {
                options.Add(command.Option9Id.Value, option9);
            }
            else { newOptions.Add(option9); }
        };
        if (!string.IsNullOrWhiteSpace(command.Option10))
        {
            var option10 = new OptionValue { Value = command.Option10, CompleteSurvey = command.CompleteSurvey10 };
            if (command.Option10Id.HasValue)
            {
                options.Add(command.Option10Id.Value, option10);
            }
            else { newOptions.Add(option10); }
        };


        if (options.Count > 0)
        {
            foreach (var option in options)
            {
                await UpsertOption.Update(_mediator, option.Key.Value, option.Value);
            }
        }

        if (newOptions.Count > 0)
        {
            foreach (var option in newOptions)
            {
                await UpsertOption.Create(_mediator, command.Id, option);
            }
        }

        // Combine the two lists and select the IDs
        var deleteRuleIds = command.VisibleRules.Select(x => x.SurveyQuestionRuleId)
            .Concat(command.RequiredRules.Select(x => x.SurveyQuestionRuleId)).ToList();

        // Get all the rules for this question whose ids are not in the delete list.
        var deleteRules = await _context.SurveyQuestionRules
            .Where(x => x.QuestionId == command.Id && !deleteRuleIds.Contains(x.SurveyQuestionRuleId))
            .ToListAsync();

        // Mark rules for deletion
        foreach (var rule in deleteRules)
        {
            _context.SurveyQuestionRules.Remove(rule);
        }
        
        
        // Insert / Update Visible rules
        foreach (var ruleCommand in command.VisibleRules)
        {     
            await _mediator.Send(ruleCommand);
        }
        
        // Insert / Update Required rules
        foreach (var ruleCommand in command.RequiredRules)
        {     
            await _mediator.Send(ruleCommand);
        }

        
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SurveyQuestionId;

        return opStatus;
    }

}
