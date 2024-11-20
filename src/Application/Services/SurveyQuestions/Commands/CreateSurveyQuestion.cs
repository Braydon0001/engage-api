using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class CreateSurveyQuestionCommand : SurveyQuestionCommand, IRequest<OperationStatus>
{
    public int SurveyId { get; set; }
}

public class CreateSurveyQuestionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyQuestionCommand, OperationStatus>
{
    public CreateSurveyQuestionCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateSurveyQuestionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSurveyQuestionCommand, SurveyQuestion>(command);

        var displayOrder = await _context.SurveyQuestions.IgnoreQueryFilters()
                                                         .Where(e => e.SurveyId == command.SurveyId)
                                                         .Where(e => e.Deleted == false)
                                                         .Where(e => e.Disabled == false)
                                                         .OrderByDescending(e => e.DisplayOrder)
                                                         .Select(e => e.DisplayOrder)
                                                         .FirstOrDefaultAsync(cancellationToken);

        if (displayOrder == null)
        {
            entity.DisplayOrder = 1;
        } else
        {
            entity.DisplayOrder = displayOrder + 1;
        }

        entity.SurveyId = command.SurveyId;

        _context.SurveyQuestions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.SurveyQuestionId;

            if (command.QuestionFalseReasonIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                    AssignDesc.FALSEREASON_SURVEYQUESTION, entity.SurveyQuestionId, command.QuestionFalseReasonIds));
            }

            var options = new List<OptionValue>();
            if (!string.IsNullOrEmpty(command.Option1)) { options.Add(new OptionValue { Value = command.Option1, CompleteSurvey = command.CompleteSurvey1 }); }
            if (!string.IsNullOrEmpty(command.Option2)) { options.Add(new OptionValue { Value = command.Option2, CompleteSurvey = command.CompleteSurvey2 }); }
            if (!string.IsNullOrEmpty(command.Option3)) { options.Add(new OptionValue { Value = command.Option3, CompleteSurvey = command.CompleteSurvey3 }); }
            if (!string.IsNullOrEmpty(command.Option4)) { options.Add(new OptionValue { Value = command.Option4, CompleteSurvey = command.CompleteSurvey4 }); }
            if (!string.IsNullOrEmpty(command.Option5)) { options.Add(new OptionValue { Value = command.Option5, CompleteSurvey = command.CompleteSurvey5 }); }
            if (!string.IsNullOrEmpty(command.Option6)) { options.Add(new OptionValue { Value = command.Option6, CompleteSurvey = command.CompleteSurvey6 }); }
            if (!string.IsNullOrEmpty(command.Option7)) { options.Add(new OptionValue { Value = command.Option7, CompleteSurvey = command.CompleteSurvey7 }); }
            if (!string.IsNullOrEmpty(command.Option8)) { options.Add(new OptionValue { Value = command.Option8, CompleteSurvey = command.CompleteSurvey8 }); }
            if (!string.IsNullOrEmpty(command.Option9)) { options.Add(new OptionValue { Value = command.Option9, CompleteSurvey = command.CompleteSurvey9 }); }
            if (!string.IsNullOrEmpty(command.Option10)) { options.Add(new OptionValue { Value = command.Option10, CompleteSurvey = command.CompleteSurvey10 }); }

            if (options.Count > 0)
            {
                foreach (var option in options)
                {
                    await UpsertOption.Create(_mediator, entity.SurveyQuestionId, option);
                }
            }

            if (command.VisibleRules != null)
            {
                // Insert / Update Visible rules
                foreach (var ruleCommand in command.VisibleRules)
                {
                    ruleCommand.QuestionId = entity.SurveyQuestionId;
                    await _mediator.Send(ruleCommand);
                }
            }

            if (command.RequiredRules != null)
            {
                // Insert / Update Required rules
                foreach (var ruleCommand in command.RequiredRules)
                {
                    ruleCommand.QuestionId = entity.SurveyQuestionId;
                    await _mediator.Send(ruleCommand);
                }
            }      

            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }

}
